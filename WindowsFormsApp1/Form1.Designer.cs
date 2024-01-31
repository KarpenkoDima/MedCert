namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.maskedTextBoxDateCheck = new System.Windows.Forms.MaskedTextBox();
            this.labelDateCheck = new System.Windows.Forms.Label();
            this.maskedTextBoxTimeCheck = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBoxBoD = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxResultMedCheck = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxResultMedCheck = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxMedExam = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxMedExam = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxMDFIO = new System.Windows.Forms.TextBox();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.checkedListBoxMD = new System.Windows.Forms.CheckedListBox();
            this.groupBoxForm = new System.Windows.Forms.GroupBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.списокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListCustomerCert = new System.Windows.Forms.ToolStripMenuItem();
            this.логToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBoxForm.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Font = new System.Drawing.Font("Calibri", 10F);
            this.textBoxFIO.Location = new System.Drawing.Point(15, 98);
            this.textBoxFIO.MaxLength = 150;
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(547, 24);
            this.textBoxFIO.TabIndex = 3;
            // 
            // maskedTextBoxDateCheck
            // 
            this.maskedTextBoxDateCheck.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBoxDateCheck.Location = new System.Drawing.Point(168, 45);
            this.maskedTextBoxDateCheck.Mask = "00/00/0000";
            this.maskedTextBoxDateCheck.Name = "maskedTextBoxDateCheck";
            this.maskedTextBoxDateCheck.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBoxDateCheck.TabIndex = 1;
            this.maskedTextBoxDateCheck.ValidatingType = typeof(System.DateTime);
            // 
            // labelDateCheck
            // 
            this.labelDateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDateCheck.Location = new System.Drawing.Point(12, 45);
            this.labelDateCheck.Name = "labelDateCheck";
            this.labelDateCheck.Size = new System.Drawing.Size(147, 26);
            this.labelDateCheck.TabIndex = 200;
            this.labelDateCheck.Text = "1. дата проведення огляду";
            // 
            // maskedTextBoxTimeCheck
            // 
            this.maskedTextBoxTimeCheck.Font = new System.Drawing.Font("Calibri", 10F);
            this.maskedTextBoxTimeCheck.Location = new System.Drawing.Point(453, 45);
            this.maskedTextBoxTimeCheck.Mask = "00:00";
            this.maskedTextBoxTimeCheck.Name = "maskedTextBoxTimeCheck";
            this.maskedTextBoxTimeCheck.Size = new System.Drawing.Size(84, 24);
            this.maskedTextBoxTimeCheck.TabIndex = 2;
            this.maskedTextBoxTimeCheck.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(298, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 26);
            this.label1.TabIndex = 202;
            this.label1.Text = "2. час проведення огляду";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(547, 18);
            this.label2.TabIndex = 203;
            this.label2.Text = "3. Прізвище, власне ім’я, по батькові (за наявності) особи, стосовно якої проводи" +
    "ться огляд:";
            // 
            // maskedTextBoxBoD
            // 
            this.maskedTextBoxBoD.Font = new System.Drawing.Font("Calibri", 10F);
            this.maskedTextBoxBoD.Location = new System.Drawing.Point(168, 135);
            this.maskedTextBoxBoD.Mask = "00/00/0000";
            this.maskedTextBoxBoD.Name = "maskedTextBoxBoD";
            this.maskedTextBoxBoD.Size = new System.Drawing.Size(100, 24);
            this.maskedTextBoxBoD.TabIndex = 4;
            this.maskedTextBoxBoD.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 26);
            this.label3.TabIndex = 206;
            this.label3.Text = "4. Дата народження";
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.DropDownWidth = 60;
            this.comboBoxSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.comboBoxSex.Location = new System.Drawing.Point(497, 135);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(40, 28);
            this.comboBoxSex.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(274, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 26);
            this.label4.TabIndex = 208;
            this.label4.Text = "5. Стать: чоловіча -1, жіноча - 2, інше - 3";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(9, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 209;
            this.label5.Text = "6. Місце проживання:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Font = new System.Drawing.Font("Calibri", 10F);
            this.textBoxAddress.Location = new System.Drawing.Point(18, 188);
            this.textBoxAddress.MaxLength = 201;
            this.textBoxAddress.Multiline = true;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(547, 51);
            this.textBoxAddress.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(10, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(547, 26);
            this.label6.TabIndex = 210;
            this.label6.Text = "7. Результати огляду.\r\nПсихіатричні, у тому числі спричинені вживанням психоактив" +
    "них речовин, протипоказань для виконання\r\n";
            // 
            // textBoxResultMedCheck
            // 
            this.textBoxResultMedCheck.Font = new System.Drawing.Font("Calibri", 10F);
            this.textBoxResultMedCheck.Location = new System.Drawing.Point(18, 284);
            this.textBoxResultMedCheck.MaxLength = 120;
            this.textBoxResultMedCheck.Multiline = true;
            this.textBoxResultMedCheck.Name = "textBoxResultMedCheck";
            this.textBoxResultMedCheck.Size = new System.Drawing.Size(547, 39);
            this.textBoxResultMedCheck.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(168, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(219, 23);
            this.label7.TabIndex = 212;
            this.label7.Text = "(вид діяльності, робіт, професій, служби)";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(6, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 19);
            this.label8.TabIndex = 213;
            this.label8.Text = "наявні — 1, відсутні — 2";
            // 
            // comboBoxResultMedCheck
            // 
            this.comboBoxResultMedCheck.DropDownWidth = 40;
            this.comboBoxResultMedCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxResultMedCheck.FormattingEnabled = true;
            this.comboBoxResultMedCheck.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxResultMedCheck.Location = new System.Drawing.Point(497, 360);
            this.comboBoxResultMedCheck.Name = "comboBoxResultMedCheck";
            this.comboBoxResultMedCheck.Size = new System.Drawing.Size(40, 28);
            this.comboBoxResultMedCheck.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(18, 392);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(547, 26);
            this.label9.TabIndex = 215;
            this.label9.Text = "8. Результати обстеження, проведеного комісією лікарів-психіатрів.\r\nПсихіатричні," +
    " у тому числі спричинені вживанням психоактивних речовин, протипоказань для вико" +
    "нання\r\n";
            // 
            // textBoxMedExam
            // 
            this.textBoxMedExam.Font = new System.Drawing.Font("Calibri", 10F);
            this.textBoxMedExam.Location = new System.Drawing.Point(18, 421);
            this.textBoxMedExam.MaxLength = 150;
            this.textBoxMedExam.Name = "textBoxMedExam";
            this.textBoxMedExam.Size = new System.Drawing.Size(547, 24);
            this.textBoxMedExam.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(158, 450);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(219, 23);
            this.label10.TabIndex = 217;
            this.label10.Text = " (вид діяльності, робіт, професій, служби)";
            // 
            // comboBoxMedExam
            // 
            this.comboBoxMedExam.DropDownWidth = 40;
            this.comboBoxMedExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxMedExam.FormattingEnabled = true;
            this.comboBoxMedExam.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxMedExam.Location = new System.Drawing.Point(497, 468);
            this.comboBoxMedExam.Name = "comboBoxMedExam";
            this.comboBoxMedExam.Size = new System.Drawing.Size(40, 28);
            this.comboBoxMedExam.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(18, 468);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(441, 26);
            this.label11.TabIndex = 219;
            this.label11.Text = "наявні — 1, відсутні — 2";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(18, 507);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(547, 26);
            this.label12.TabIndex = 221;
            this.label12.Text = "9. Прізвище, власне ім’я, по батькові (за наявності) лікаря-психіатра (комісії лі" +
    "карів-психіатрів),\r\nякий(а) проводив(ла) огляд:";
            // 
            // textBoxMDFIO
            // 
            this.textBoxMDFIO.Font = new System.Drawing.Font("Calibri", 10F);
            this.textBoxMDFIO.Location = new System.Drawing.Point(18, 539);
            this.textBoxMDFIO.Name = "textBoxMDFIO";
            this.textBoxMDFIO.Size = new System.Drawing.Size(547, 24);
            this.textBoxMDFIO.TabIndex = 11;
            this.textBoxMDFIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(403, 644);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(157, 57);
            this.buttonPrint.TabIndex = 222;
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkedListBoxMD
            // 
            this.checkedListBoxMD.FormattingEnabled = true;
            this.checkedListBoxMD.Items.AddRange(new object[] {
            "Валерий Пироженко",
            "Смаль Евгений Эдуардович",
            "Артур Пирожков",
            "Валерий Пироженко",
            "Смаль Евгений Эдуардович",
            "Артур Пирожков",
            "Валерий Пироженко",
            "Смаль Евгений Эдуардович",
            "Артур Пирожков"});
            this.checkedListBoxMD.Location = new System.Drawing.Point(18, 571);
            this.checkedListBoxMD.Name = "checkedListBoxMD";
            this.checkedListBoxMD.Size = new System.Drawing.Size(544, 67);
            this.checkedListBoxMD.TabIndex = 223;
            // 
            // groupBoxForm
            // 
            this.groupBoxForm.Controls.Add(this.buttonClear);
            this.groupBoxForm.Controls.Add(this.comboBox1);
            this.groupBoxForm.Controls.Add(this.button1);
            this.groupBoxForm.Controls.Add(this.checkedListBoxMD);
            this.groupBoxForm.Controls.Add(this.buttonPrint);
            this.groupBoxForm.Controls.Add(this.textBoxMDFIO);
            this.groupBoxForm.Controls.Add(this.label12);
            this.groupBoxForm.Controls.Add(this.label11);
            this.groupBoxForm.Controls.Add(this.comboBoxMedExam);
            this.groupBoxForm.Controls.Add(this.label10);
            this.groupBoxForm.Controls.Add(this.textBoxMedExam);
            this.groupBoxForm.Controls.Add(this.label9);
            this.groupBoxForm.Controls.Add(this.comboBoxResultMedCheck);
            this.groupBoxForm.Controls.Add(this.label8);
            this.groupBoxForm.Controls.Add(this.label7);
            this.groupBoxForm.Controls.Add(this.textBoxResultMedCheck);
            this.groupBoxForm.Controls.Add(this.label6);
            this.groupBoxForm.Controls.Add(this.textBoxAddress);
            this.groupBoxForm.Controls.Add(this.label5);
            this.groupBoxForm.Controls.Add(this.label4);
            this.groupBoxForm.Controls.Add(this.comboBoxSex);
            this.groupBoxForm.Controls.Add(this.label3);
            this.groupBoxForm.Controls.Add(this.maskedTextBoxBoD);
            this.groupBoxForm.Controls.Add(this.label2);
            this.groupBoxForm.Controls.Add(this.label1);
            this.groupBoxForm.Controls.Add(this.maskedTextBoxTimeCheck);
            this.groupBoxForm.Controls.Add(this.labelDateCheck);
            this.groupBoxForm.Controls.Add(this.maskedTextBoxDateCheck);
            this.groupBoxForm.Controls.Add(this.textBoxFIO);
            this.groupBoxForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxForm.Location = new System.Drawing.Point(12, 12);
            this.groupBoxForm.Name = "groupBoxForm";
            this.groupBoxForm.Size = new System.Drawing.Size(580, 727);
            this.groupBoxForm.TabIndex = 0;
            this.groupBoxForm.TabStop = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(186, 644);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(157, 57);
            this.buttonClear.TabIndex = 226;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Діяльності працівника залізничного тр-ту",
            "Діяльності водія кат.",
            "Військової служби у ЗСУ з доступом до державної таємниці",
            "Зберігання, носіння та використання вогнепальної зброї"});
            this.comboBox1.Location = new System.Drawing.Point(411, 327);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(154, 28);
            this.comboBox1.TabIndex = 225;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 644);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 57);
            this.button1.TabIndex = 224;
            this.button1.Text = "Доктора";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonMD_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокToolStripMenuItem,
            this.логToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(604, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // списокToolStripMenuItem
            // 
            this.списокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListCustomerCert});
            this.списокToolStripMenuItem.Name = "списокToolStripMenuItem";
            this.списокToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.списокToolStripMenuItem.Text = "Список ";
            // 
            // ListCustomerCert
            // 
            this.ListCustomerCert.Name = "ListCustomerCert";
            this.ListCustomerCert.Size = new System.Drawing.Size(254, 22);
            this.ListCustomerCert.Text = "Список выданных сертификатов";
            this.ListCustomerCert.Click += new System.EventHandler(this.ListCustomerCert_Click);
            // 
            // логToolStripMenuItem
            // 
            this.логToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.логиToolStripMenuItem});
            this.логToolStripMenuItem.Name = "логToolStripMenuItem";
            this.логToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.логToolStripMenuItem.Text = "Лог";
            // 
            // логиToolStripMenuItem
            // 
            this.логиToolStripMenuItem.Name = "логиToolStripMenuItem";
            this.логиToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.логиToolStripMenuItem.Text = "Логи";
            this.логиToolStripMenuItem.Click += new System.EventHandler(this.Log_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(604, 751);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBoxForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximumSize = new System.Drawing.Size(620, 790);
            this.MinimumSize = new System.Drawing.Size(620, 790);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBoxForm.ResumeLayout(false);
            this.groupBoxForm.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox comboBoxMedExam;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxMDFIO;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox checkedListBoxMD;

        private System.Windows.Forms.ErrorProvider errorProvider1;

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxMedExam;

        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.ComboBox comboBoxResultMedCheck;

        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.TextBox textBoxResultMedCheck;
        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.TextBox textBoxAddress;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.MaskedTextBox maskedTextBoxBoD;
        private System.Windows.Forms.ComboBox comboBoxSex;

       private System.Windows.Forms.Label label3;
        //private System.Windows.Forms.MaskedTextBox maskedTextBox1;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.MaskedTextBox maskedTextBoxTimeCheck;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Label labelDateCheck;

        private System.Windows.Forms.MaskedTextBox maskedTextBoxDateCheck;

        private System.Windows.Forms.TextBox textBoxFIO;

        private System.Windows.Forms.GroupBox groupBoxForm;

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem списокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ListCustomerCert;
        private System.Windows.Forms.ToolStripMenuItem логToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem логиToolStripMenuItem;
    }
}