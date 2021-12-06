using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ADALogAnalisator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeLogViewer();
            //openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return; 
            // получаем выбранный файл
            string[] filenames = openFileDialog1.FileNames;
            // читаем файл в строку

            oLogViewer.setPathToFiles(filenames);
            MessageBox.Show("Файл/ы открыт/ы");
            tFileNames.Clear();
            foreach(string fileName in filenames)
            {
                tFileNames.Text += (fileName + "\r\n");
            }
        }

        private void setDN_TextChanged(object sender, EventArgs e)
        {
            oLogViewer.setDN(textBoxDN.Text);
        }

        private void setTN_TextChanged(object sender, EventArgs e)
        {
            oLogViewer.setDN(textBoxTN.Text);
        }

        private void Analiz_Click(object sender, EventArgs e)
        {
            /* TODO:
             * Проверь указал ли пользователь искомые DN и TN.
             * Если нет - попросить указать отдельным MessageBox.
             */
            oLogViewer.AnalizeFiles();
        }
    }
}
