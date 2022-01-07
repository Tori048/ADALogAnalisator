﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ADALogAnalisator
{

    class LogViewer
    {

        public void setPathToFiles(string[] text)
        {
            FileForAnaliz = text;
        }
        public void setSetIdentificators(string DN, string TN)
        {
            DN_TN.sDN = DN;
            DN_TN.sTN = TN;
        }
        public string getDN()
        {
            return DN_TN.sDN;
        }
        public string getTN()
        {
            return DN_TN.sTN;
        }
        public void setDN(string DN)
        {
            DN_TN.sDN = DN;
        }
        public void setTN(string TN)
        {
            DN_TN.sTN = TN;
        }
        static object locker = new object();
        static object DictionarLock = new object();

        public bool SelectUnregReason
        {
            set { selectUnregReason = value; }
            get { return selectUnregReason; }
        }
           
        struct SetIdentificators
        {
            public string sDN;
            public string sTN;
        }

        private SetIdentificators DN_TN;
        private bool selectUnregReason;
        private string[] FileForAnaliz;

        /* Список всех endpoint, что были найдены.
         * 1st string - TN, 2nd string - DN 
         */
        private Dictionary<string, string> dEndpoints = new Dictionary<string, string>();

        //Только для того, чтобы в консоль выводилось то, что было найдено
        private void FindDNTN(string str)
        {
            System.Diagnostics.Trace.WriteLine(str);
        }


        /* Добавляет endpoint и данные с ним связанные во все словари, что есть.
         * 
         */
        private void addEndpointToDictionaries(string TN, string DN = "", string TimeReg = "", string TimeUnreg = "")
        {
            //KeyValuePair<string, string> TnDn = new KeyValuePair<string, string>(TN, DN);
            //Добавим endpoint в словарь всех endpoint, хотя бы TN.
            if (!dEndpoints.ContainsKey(TN))
            {
                dEndpoints.Add(TN, DN);
            }
            //Додобавим туда же DN, если вдруг его нет для TN.
            else if (dEndpoints.ContainsKey(TN) && dEndpoints[TN] != "" && DN != "")
            {
                dEndpoints[TN] = DN;
            }
        }

        private void FindUnregReason(string sLine)
        {
            if (sLine.Contains("CEndpointManager::OnEndpointUnregistered"))
            {
                string TN = sLine.Substring(sLine.IndexOf("TN ")+3, (sLine.LastIndexOf(",") - sLine.IndexOf("TN "))-3);
                addEndpointToDictionaries(TN);
            }
        }

        private void ReadFile(object FileName)
        {
            //Console.WriteLine("kek");
            string sFileName = (string)FileName;
            using (StreamReader sr = new StreamReader(sFileName, System.Text.Encoding.Default))
            {
                Trace.WriteLine(FileName + " threadID = " + Thread.CurrentThread.ManagedThreadId);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    /*TODO: тут надо почитать файл построчно и поискать выбранные DN и TN.
                    1. Если находится нужный DN/TN - запоминается время и название сервера.
                    2. Если телефон разрегистрировался - запоминается причина разрегистрации.
                    */
                    if (SelectUnregReason)
                    {/*
                        если line содержит что-то, что будет указывать на TN или DN, то тогда надо закинуть их в словарь с endpints.
                        */
                        lock (DictionarLock)
                        {
                            FindUnregReason(line);
                        }
                    }
                    if (line.Contains(getDN()) || line.Contains(getTN()))
                    {
                        
                        FindDNTN(line);
                        writeTextToFile(line);
                    }
                }
            }
        }

        private void writeTextToFile(string Text)
        {
            string FileName = Path.Combine(Environment.CurrentDirectory, "fingingDetails.txt");
            lock(locker)
            {
                using (StreamWriter sw = new StreamWriter(FileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(Text);
                }
            }
        }

        public void AnalizeFiles()
        {
            Trace.WriteLine( "ProcCount = " + Environment.ProcessorCount);
            /*
             * Проверь корректно ли будут работать потоки. Сделай пул потоков
             * Возможно, надо засунуть имена файлов в какой-то список, где можно будет маркать прочитанные или открытые. Или просто добавить проверку
             * то файл не открыт прямо сейчас кем то другим
             * 
             * 
             */
            // Open the stream and read it back.
            foreach (string FileName in FileForAnaliz)
            {
                Trace.WriteLine(FileName);
                Thread myThread = new Thread(new ParameterizedThreadStart(ReadFile));
                myThread.Start(FileName);
            }
        }
    }
}
