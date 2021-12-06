using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ADALogAnalisator
{

    class LogViewer
    {
        struct SetIdentificators
        {
            
            public string sDN;
            public string sTN;
        }
        private SetIdentificators DN_TN;
        private string[] FileForAnaliz;

        public void setPathToFiles(string[] text)
        {
            FileForAnaliz = text;
        }

        public void setSetIdentificators(string DN, string TN)
        {
            DN_TN.sDN = DN;
            DN_TN.sTN = TN;
        }
        public void setDN(string DN)
        {
            DN_TN.sDN = DN;
        }
        public void setTN(string TN)
        {
            DN_TN.sTN = TN;
        }
        public void AnalizeFiles()
        {
            // Open the stream and read it back.
            foreach (string FileName in FileForAnaliz)
            {
                using (FileStream fs = File.Open(FileName, FileMode.Open))
                {
                    /*TODO: тут надо почитать файл построчно и поискать выбранные DN и TN.
                    0. подумать над тем, как разделить чтение файла на несколько потоков. Или же для каждого файла выделить отдельный поток. 
                    1. Если находится нужный DN/TN - запоминается время и название сервера.
                    2. Если телефон разрегистрировался - запоминается причина разрегистрации.
                    */
                }
            }
        }
    }
}
