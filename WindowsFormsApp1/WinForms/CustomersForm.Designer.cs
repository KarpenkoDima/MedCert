namespace WindowsFormsApp1.WinForms
{
    partial class CustomersForm
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
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.groupBoxCustomers = new System.Windows.Forms.GroupBox();
            this.buttonGetAllCustomers = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearchFIO = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            this.groupBoxCustomers.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(7, 32);
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.Size = new System.Drawing.Size(971, 458);
            this.dataGridViewCustomers.TabIndex = 0;
            this.dataGridViewCustomers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomers_CellClick);
            // 
            // groupBoxCustomers
            // 
            this.groupBoxCustomers.Controls.Add(this.buttonGetAllCustomers);
            this.groupBoxCustomers.Controls.Add(this.buttonExit);
            this.groupBoxCustomers.Controls.Add(this.buttonSearch);
            this.groupBoxCustomers.Controls.Add(this.labelSearch);
            this.groupBoxCustomers.Controls.Add(this.textBoxSearchFIO);
            this.groupBoxCustomers.Controls.Add(this.dataGridViewCustomers);
            this.groupBoxCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCustomers.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCustomers.Name = "groupBoxCustomers";
            this.groupBoxCustomers.Size = new System.Drawing.Size(984, 561);
            this.groupBoxCustomers.TabIndex = 1;
            this.groupBoxCustomers.TabStop = false;
            this.groupBoxCustomers.Text = "Список кому выдан сертификат";
            // 
            // buttonGetAllCustomers
            // 
            this.buttonGetAllCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetAllCustomers.Location = new System.Drawing.Point(7, 494);
            this.buttonGetAllCustomers.Name = "buttonGetAllCustomers";
            this.buttonGetAllCustomers.Size = new System.Drawing.Size(75, 59);
            this.buttonGetAllCustomers.TabIndex = 5;
            this.buttonGetAllCustomers.Text = "Показать всех";
            this.buttonGetAllCustomers.UseVisualStyleBackColor = true;
            this.buttonGetAllCustomers.Click += new System.EventHandler(this.buttonGetAllCustomers_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(897, 494);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 59);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Закрыть";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSearch.Location = new System.Drawing.Point(584, 514);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Искать";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelSearch
            // 
            this.labelSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(238, 519);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(108, 13);
            this.labelSearch.TabIndex = 2;
            this.labelSearch.Text = "Искать по фамилии";
            // 
            // textBoxSearchFIO
            // 
            this.textBoxSearchFIO.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxSearchFIO.Location = new System.Drawing.Point(358, 514);
            this.textBoxSearchFIO.Name = "textBoxSearchFIO";
            this.textBoxSearchFIO.Size = new System.Drawing.Size(220, 20);
            this.textBoxSearchFIO.TabIndex = 1;
            this.textBoxSearchFIO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearchFIO_KeyPress);
            // 
            // CustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.groupBoxCustomers);
            this.Name = "CustomersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomersForm";
            this.Load += new System.EventHandler(this.CustomersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            this.groupBoxCustomers.ResumeLayout(false);
            this.groupBoxCustomers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.GroupBox groupBoxCustomers;
        private System.Windows.Forms.Button buttonGetAllCustomers;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearchFIO;
    }
}