namespace Курсовая_ТФЯТ
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LoadTestButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.programString = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.errorsTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.generationBox = new System.Windows.Forms.RichTextBox();
            this.polizBox = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AnalysisBox = new System.Windows.Forms.ListBox();
            this.DutyWordBox = new System.Windows.Forms.ListBox();
            this.DevidedWordBox = new System.Windows.Forms.ListBox();
            this.ConstBox = new System.Windows.Forms.ListBox();
            this.IdBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.LoadTestButton);
            this.groupBox1.Controls.Add(this.RunButton);
            this.groupBox1.Controls.Add(this.programString);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Zamenhof Inline", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Текст";
            // 
            // LoadTestButton
            // 
            this.LoadTestButton.BackColor = System.Drawing.SystemColors.Info;
            this.LoadTestButton.Font = new System.Drawing.Font("Zamenhof Inline", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadTestButton.Location = new System.Drawing.Point(84, 423);
            this.LoadTestButton.Name = "LoadTestButton";
            this.LoadTestButton.Size = new System.Drawing.Size(157, 40);
            this.LoadTestButton.TabIndex = 2;
            this.LoadTestButton.Text = "Загрузить тесты";
            this.LoadTestButton.UseVisualStyleBackColor = false;
            this.LoadTestButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // RunButton
            // 
            this.RunButton.BackColor = System.Drawing.SystemColors.Info;
            this.RunButton.Location = new System.Drawing.Point(294, 423);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(157, 40);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Проверка";
            this.RunButton.UseVisualStyleBackColor = false;
            this.RunButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // programString
            // 
            this.programString.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.programString.Location = new System.Drawing.Point(6, 21);
            this.programString.Name = "programString";
            this.programString.Size = new System.Drawing.Size(566, 390);
            this.programString.TabIndex = 0;
            this.programString.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.errorsTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Zamenhof Inline", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(12, 496);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(593, 216);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ошибки";
            // 
            // errorsTextBox
            // 
            this.errorsTextBox.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorsTextBox.Location = new System.Drawing.Point(6, 21);
            this.errorsTextBox.Name = "errorsTextBox";
            this.errorsTextBox.Size = new System.Drawing.Size(581, 166);
            this.errorsTextBox.TabIndex = 4;
            this.errorsTextBox.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.generationBox);
            this.groupBox3.Controls.Add(this.polizBox);
            this.groupBox3.Font = new System.Drawing.Font("Zamenhof Inline", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(611, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(677, 416);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Генерация кода";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "ПОЛИЗ";
            // 
            // generationBox
            // 
            this.generationBox.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generationBox.Location = new System.Drawing.Point(354, 38);
            this.generationBox.Name = "generationBox";
            this.generationBox.Size = new System.Drawing.Size(292, 359);
            this.generationBox.TabIndex = 6;
            this.generationBox.Text = "";
            // 
            // polizBox
            // 
            this.polizBox.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.polizBox.Location = new System.Drawing.Point(29, 38);
            this.polizBox.Name = "polizBox";
            this.polizBox.Size = new System.Drawing.Size(290, 359);
            this.polizBox.TabIndex = 5;
            this.polizBox.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.AnalysisBox);
            this.groupBox4.Controls.Add(this.DutyWordBox);
            this.groupBox4.Controls.Add(this.DevidedWordBox);
            this.groupBox4.Controls.Add(this.ConstBox);
            this.groupBox4.Controls.Add(this.IdBox);
            this.groupBox4.Location = new System.Drawing.Point(611, 418);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(677, 294);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Zamenhof Inline", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(539, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "Анализатор";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Zamenhof Inline", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(425, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "const";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Zamenhof Inline", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(324, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Zamenhof Inline", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(145, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Разделители";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Zamenhof Inline", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(26, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Служебные";
            // 
            // AnalysisBox
            // 
            this.AnalysisBox.FormattingEnabled = true;
            this.AnalysisBox.ItemHeight = 16;
            this.AnalysisBox.Location = new System.Drawing.Point(530, 39);
            this.AnalysisBox.Name = "AnalysisBox";
            this.AnalysisBox.Size = new System.Drawing.Size(132, 244);
            this.AnalysisBox.TabIndex = 8;
            // 
            // DutyWordBox
            // 
            this.DutyWordBox.FormattingEnabled = true;
            this.DutyWordBox.ItemHeight = 16;
            this.DutyWordBox.Items.AddRange(new object[] {
            "program",
            "var",
            "begin",
            "end",
            "dim",
            "integer",
            "real",
            "boolean",
            "by",
            "else",
            "do",
            "while",
            "loop",
            "for",
            "to",
            "input",
            "output"});
            this.DutyWordBox.Location = new System.Drawing.Point(25, 39);
            this.DutyWordBox.Name = "DutyWordBox";
            this.DutyWordBox.Size = new System.Drawing.Size(120, 244);
            this.DutyWordBox.TabIndex = 4;
            // 
            // DevidedWordBox
            // 
            this.DevidedWordBox.FormattingEnabled = true;
            this.DevidedWordBox.ItemHeight = 16;
            this.DevidedWordBox.Items.AddRange(new object[] {
            ":=",
            ",",
            ";",
            "+",
            "-",
            "*",
            "/",
            "(",
            ")",
            "}",
            "{",
            "<",
            ">",
            "<=",
            "!=",
            ">=",
            "=="});
            this.DevidedWordBox.Location = new System.Drawing.Point(148, 39);
            this.DevidedWordBox.Name = "DevidedWordBox";
            this.DevidedWordBox.Size = new System.Drawing.Size(120, 244);
            this.DevidedWordBox.TabIndex = 5;
            this.DevidedWordBox.SelectedIndexChanged += new System.EventHandler(this.DevidedWordBox_SelectedIndexChanged);
            // 
            // ConstBox
            // 
            this.ConstBox.FormattingEnabled = true;
            this.ConstBox.ItemHeight = 16;
            this.ConstBox.Location = new System.Drawing.Point(404, 39);
            this.ConstBox.Name = "ConstBox";
            this.ConstBox.Size = new System.Drawing.Size(120, 244);
            this.ConstBox.TabIndex = 7;
            // 
            // IdBox
            // 
            this.IdBox.FormattingEnabled = true;
            this.IdBox.ItemHeight = 16;
            this.IdBox.Location = new System.Drawing.Point(278, 39);
            this.IdBox.Name = "IdBox";
            this.IdBox.Size = new System.Drawing.Size(120, 244);
            this.IdBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1313, 735);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Курсовая работа ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.RichTextBox programString;
        private System.Windows.Forms.ListBox DutyWordBox;
        private System.Windows.Forms.ListBox DevidedWordBox;
        private System.Windows.Forms.ListBox IdBox;
        private System.Windows.Forms.ListBox ConstBox;
        private System.Windows.Forms.ListBox AnalysisBox;
        private System.Windows.Forms.RichTextBox errorsTextBox;
        private System.Windows.Forms.RichTextBox generationBox;
        private System.Windows.Forms.RichTextBox polizBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LoadTestButton;
    }
}

