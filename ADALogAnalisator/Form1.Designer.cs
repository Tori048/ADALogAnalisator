namespace ADALogAnalisator
{
    partial class Form1
    {
        private void InitializeLogViewer()
        {
            this.oLogViewer = new LogViewer();
        }
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxDN = new System.Windows.Forms.TextBox();
            this.textBoxTN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tFileNames = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выбрать файлы";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // textBoxDN
            // 
            this.textBoxDN.Location = new System.Drawing.Point(12, 374);
            this.textBoxDN.Name = "textBoxDN";
            this.textBoxDN.Size = new System.Drawing.Size(100, 20);
            this.textBoxDN.TabIndex = 1;
            this.textBoxDN.TextChanged += new System.EventHandler(this.setDN_TextChanged);
            // 
            // textBoxTN
            // 
            this.textBoxTN.Location = new System.Drawing.Point(196, 374);
            this.textBoxTN.Name = "textBoxTN";
            this.textBoxTN.Size = new System.Drawing.Size(100, 20);
            this.textBoxTN.TabIndex = 2;
            this.textBoxTN.TextChanged += new System.EventHandler(this.setTN_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите DN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите TN";
            // 
            // tFileNames
            // 
            this.tFileNames.Location = new System.Drawing.Point(567, 47);
            this.tFileNames.Multiline = true;
            this.tFileNames.Name = "tFileNames";
            this.tFileNames.ReadOnly = true;
            this.tFileNames.Size = new System.Drawing.Size(221, 324);
            this.tFileNames.TabIndex = 5;
            this.tFileNames.Text = "Здесь отобразятся имена выбранных вами файлов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(564, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Выбранны файлы:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(567, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Проанализировать выбранные файлы";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Analiz_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(210, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Запомнить причину разрегистрации";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBoxSelectUnregReason);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tFileNames);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTN);
            this.Controls.Add(this.textBoxDN);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBoxDN;
        private System.Windows.Forms.TextBox textBoxTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private LogViewer oLogViewer;// = new LogViewer();
        private System.Windows.Forms.TextBox tFileNames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

