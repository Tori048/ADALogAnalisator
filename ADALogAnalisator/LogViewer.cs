using System;
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
        private Dictionary<string, string> dAllEndpoints = new Dictionary<string, string>();
        /* Список со временем разрега и рега каждого endpoint
         * 1st string - TN, 2nd string - <время разрегистрации;причина>, 3nd - время регистрации
         */ 
        private Dictionary<string, List<KeyValuePair<string, string>>> dUnregReg = new Dictionary<string, List<KeyValuePair<string, string>>>();

        //Только для того, чтобы в консоль выводилось то, что было найдено
        private void FindDNTN(string str)
        {
            System.Diagnostics.Trace.WriteLine(Thread.CurrentThread.Name + ":" + str + " threadID = " + Thread.CurrentThread.ManagedThreadId);
        }

        /* Добавляет endpoint и данные с ним связанные во все словари, что есть. 
         */
        private void addEndpointToDictionaries(string TN, string DN = "", string TimeReg = "", string TimeUnreg = "", string Reason = "")
        {
            //KeyValuePair<string, string> TnDn = new KeyValuePair<string, string>(TN, DN);
            //Добавим endpoint в словарь всех endpoint, хотя бы TN.
            if (!dAllEndpoints.ContainsKey(TN))
            {
                dAllEndpoints.Add(TN, DN);
            }
            //Додобавим туда же DN, если вдруг его нет для TN.
            /*else*/ if (dAllEndpoints.ContainsKey(TN) && dAllEndpoints[TN] != "" && DN != "")
            {
                dAllEndpoints[TN] = DN;
            }
            /* Добавим в словарь со статистикой информацию о endpoint
            */
            if (!dUnregReg.ContainsKey(TN))
            {
                if (TimeUnreg != "" && Reason != "")
                {
                    KeyValuePair<string, string> test = new KeyValuePair<string, string>(TimeUnreg + ";" + Reason, "");
                    List<KeyValuePair<string, string>> lUnregAndReason = new List<KeyValuePair<string, string>>() { test};
                    dUnregReg.Add(TN, lUnregAndReason);
                }
                else if (TimeReg != "")
                {
                    KeyValuePair<string, string> test = new KeyValuePair<string, string>("", TimeReg);
                    List<KeyValuePair<string, string>> lReg = new List<KeyValuePair<string, string>>() { test };
                    dUnregReg.Add(TN, lReg);
                }
            }
            else if(dUnregReg.ContainsKey(TN))
            {
                /*TODO:
                 * сделать логику добавления информации о endpoint, если TN уже есть, т.е. продолжить заполнение статистических данных.
                 */
            }
            if (TimeReg!= "")
            {
                if (!dUnregReg.ContainsKey(TN))
                {
                    KeyValuePair<string, string> test = new KeyValuePair<string, string>("", TimeReg);
                    List<KeyValuePair<string, string>> lUnregAndReason = new List<KeyValuePair<string, string>>() { test };
                    dUnregReg.Add(TN, lUnregAndReason);
                }
                else if (dUnregReg.ContainsKey(TN))
                {
                    /*TODO:
                    сделать логику добрасывания времени + его сортировку*/
                }
            }
            
        }

        private int getMonth(string sMonth)
        {
            int iMonth = 0;
            switch(sMonth)
            {
                case "Jan":
                    iMonth = 1;
                    break;
                case "Feb":
                    iMonth = 2;
                    break;
                case "Mar":
                    iMonth = 3;
                    break;
                case "Apr":
                    iMonth = 4;
                    break;
                case "May":
                    iMonth = 5;
                    break;
                case "Jun":
                    iMonth = 6;
                    break;
                case "Jul":
                    iMonth = 7;
                    break;
                case "Aug":
                    iMonth = 8;
                    break;
                case "Sep":
                    iMonth = 9;
                    break;
                case "Oct":
                    iMonth = 10;
                    break;
                case "Nov":
                    iMonth = 11;
                    break;
                case "Dec":
                    iMonth = 12;
                    break;
            }
            return iMonth;
        }

        private void FindUnregTimeAndReason(string sLine)
        {
            if (sLine.Contains("CEndpointManager::OnEndpointUnregistered"))
            {
                string TN = sLine.Substring(sLine.IndexOf("TN ")+3, (sLine.LastIndexOf(",") - sLine.IndexOf("TN "))-3);
                string UnregMonth = sLine.Substring(0, sLine.IndexOf(" "));
                int iMonth = getMonth(UnregMonth);
                string UnregDay = sLine.Substring(sLine.IndexOf(" ") + 1, 2);
                int iDay = Int32.Parse(UnregDay);
                string UnregTime = sLine.Substring(sLine.IndexOf(":") - 2, 8);
                int iHour = Int32.Parse(UnregTime.Substring(UnregTime.IndexOf(":") - 2, 2));
                int iMin = Int32.Parse(UnregTime.Substring(UnregTime.IndexOf(":") + 1, 2));
                int iSec = Int32.Parse(UnregTime.Substring(UnregTime.LastIndexOf(":") + 1, 2));
                string Reason = sLine.Substring(sLine.IndexOf("=") + 2, (sLine.LastIndexOf("(") - sLine.IndexOf("="))-3);
                DateTime UnregisterTime = new DateTime(1984, iMonth, iDay, iHour, iMin, iSec);
                /* ToDO:
                 * Прикрути использование DateTime к addEndpointToDictionaries, а то как в деревне*/
               // addEndpointToDictionaries(TN,"","",UnregTime,Reason);
            }
        }

        private void FindRegTime(string sLine)
        {
            if (sLine.Contains("CEndpointManager::OnEndpointRegistrationSuccessful"))
            {
                string TN = sLine.Substring(sLine.IndexOf("TN ") + 3, (sLine.LastIndexOf("(") - sLine.IndexOf("TN ")) - 4);
                string RegDay = sLine.Substring(sLine.IndexOf(" ") - 3, sLine.IndexOf(":") - 3);
                string RegTime = RegDay + "/" + sLine.Substring(sLine.IndexOf(":") - 2, 8);
                addEndpointToDictionaries(TN, "", RegTime);
            }
        }

        private void ReadFile(object FileName)
        {
            //Console.WriteLine("kek");
            string sFileName = (string)FileName;
            using (StreamReader sr = new StreamReader(sFileName, System.Text.Encoding.Default))
            { 
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
                            FindUnregTimeAndReason(line);
                            FindRegTime(line);
                        }
                    }
                    if (line.Contains(getDN()) || line.Contains(getTN()))
                    {
                        
                       // FindDNTN(line);
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
