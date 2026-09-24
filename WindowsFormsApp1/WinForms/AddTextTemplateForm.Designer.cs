using System.ComponentModel;

namespace WindowsFormsApp1.WinForms
{
    partial class AddTextTemplateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProviderAddTemplate = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCategory);
            this.groupBox1.Controls.Add(this.textBoxText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 175);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Новый шаблон";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(77, 49);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(218, 21);
            this.comboBoxCategory.TabIndex = 15;
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(77, 89);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(218, 20);
            this.textBoxText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Категория";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Текст";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 203);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(83, 46);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(249, 203);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 46);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // errorProviderAddTemplate
            // 
            this.errorProviderAddTemplate.ContainerControl = this;
            // 
            // AddTextTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 261);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(366, 300);
            this.MinimumSize = new System.Drawing.Size(366, 300);
            this.Name = "AddTextTemplateForm";
            this.Text = "Шаблоны заключений";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ErrorProvider errorProviderAddTemplate;

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCategory;
    }
}