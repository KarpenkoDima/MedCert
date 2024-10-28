using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1.WinForms
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            try
            {
                /*using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
                {
                    var customers = db.GetCollection<Customer>("Customers");

                    var result = customers.Query().ToList();
                    dataGridViewCustomers.DataSource = null;
                    dataGridViewCustomers.Rows.Clear();
                    dataGridViewCustomers.Columns.Clear();
                    // dataGridView1.AutoGenerateColumns = true;
                    dataGridViewCustomers.AllowUserToAddRows = true;
                    dataGridViewCustomers.DataSource = result;
                    dataGridViewCustomers.Columns["Index"].Visible = false;
                    dataGridViewCustomers.Columns["Id"].Visible = false;
                    dataGridViewCustomers.Columns["FIO"].HeaderText = "ПІБ";
                    dataGridViewCustomers.Columns["R1"].HeaderText = "к 7п. Наявні/Відсутні";
                    dataGridViewCustomers.Columns["R2"].HeaderText = "к 8п. Наявні/Відсутні";
                    dataGridViewCustomers.Columns["Sex"].HeaderText = "Пол";
                    dataGridViewCustomers.Columns["BoD"].HeaderText = "Д/Р";
                    dataGridViewCustomers.Columns["MedDate"].HeaderText = "Дата осмотра";
                    dataGridViewCustomers.Columns["Time"].HeaderText = "Время осмотра";
                    dataGridViewCustomers.Columns["Registration"].HeaderText = "6-Адрес";
                    dataGridViewCustomers.Columns["MedCheck"].HeaderText = "7-Результат огляду";
                    dataGridViewCustomers.Columns["MedAnalisys"].HeaderText = "8-Результат обстеження";



                    for (int i = 0; i < dataGridViewCustomers.Columns.Count; i++)
                    {
                        dataGridViewCustomers.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    DataGridViewButtonColumn detailsCol = new DataGridViewButtonColumn();
                    detailsCol.Name = "Delete";
                    detailsCol.Text = "Удалить запись";
                    detailsCol.UseColumnTextForButtonValue = true;
                    detailsCol.HeaderText = "";
                    dataGridViewCustomers.Columns.Insert(dataGridViewCustomers.Columns.Count, detailsCol);
                    DataGridViewButtonColumn detailsCol2 = new DataGridViewButtonColumn();
                    detailsCol2.Name = "Print";
                    detailsCol2.Text = "Распечатать сертификат повторно";
                    detailsCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    detailsCol2.UseColumnTextForButtonValue = true;
                    detailsCol2.HeaderText = "";
                    dataGridViewCustomers.Columns.Insert(dataGridViewCustomers.Columns.Count, detailsCol2);
                }*/
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при работе с клиентом {Environment.NewLine} Лучше перезапустите программу.",
                    "Ошибка при работе с клиентом",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);                
                LogError(ex.Message);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            CustomerPrint = null;
            this.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void SearchCustomer()
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                // Get customer collection
                var customers = db.GetCollection<Customer>("Customers");
                int count = 0;
                if (false == string.IsNullOrEmpty(textBoxSearchFIO.Text))
                {
                    var result = customers.Find(Query.StartsWith("FIO", textBoxSearchFIO.Text)).ToList();
                    dataGridViewCustomers.DataSource = result;
                    dataGridViewCustomers.Update();
                    dataGridViewCustomers.Refresh();
                    count = result.Count;
                }
                //labelResultSearch.Text = "Найдено: " + count.ToString();
            }
        }

        private void buttonGetAllCustomers_Click(object sender, EventArgs e)
        {
            CustomerPrint = null;
            this.LoadCustomers();
        }
        private void LoadCustomers()
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"]
                              .ConnectionString))
            {
                // Get customer collection

                var customers = db.GetCollection<Customer>("Customers");

                var result = customers.Query().ToList();
                dataGridViewCustomers.DataSource = null;
                dataGridViewCustomers.Rows.Clear();
                dataGridViewCustomers.Columns.Clear();
                // dataGridView1.AutoGenerateColumns = true;
                dataGridViewCustomers.AllowUserToAddRows = true;
                dataGridViewCustomers.DataSource = result;
                dataGridViewCustomers.Columns["Index"].Visible = true;
                dataGridViewCustomers.Columns["Index"].HeaderText = "Временной штамп";
                dataGridViewCustomers.Columns["Id"].Visible = false;
                dataGridViewCustomers.Columns["FIO"].HeaderText = "ПІБ";
                dataGridViewCustomers.Columns["FIO"].MinimumWidth = 100;
                dataGridViewCustomers.Columns["R1"].HeaderText = "к 7п. Наявні/Відсутні";
                dataGridViewCustomers.Columns["R2"].HeaderText = "к 8п. Наявні/Відсутні";
                dataGridViewCustomers.Columns["Sex"].HeaderText = "Пол";
                dataGridViewCustomers.Columns["BoD"].HeaderText = "Д/Р";
                dataGridViewCustomers.Columns["MedDate"].HeaderText = "Дата осмотра";
                dataGridViewCustomers.Columns["Time"].HeaderText = "Время осмотра";
                dataGridViewCustomers.Columns["Time"].DefaultCellStyle.Format = "hh:mm";
                dataGridViewCustomers.Columns["Registration"].HeaderText = "6-Адрес";
                dataGridViewCustomers.Columns["MedCheck"].HeaderText = "7-Результат огляду";
                dataGridViewCustomers.Columns["MedAnalisys"].HeaderText = "8-Результат обстеження";
                dataGridViewCustomers.Columns["MedDoctors"].HeaderText = "Лікарі";

                for (int i = 0; i < dataGridViewCustomers.Columns.Count; i++)
                {
                    dataGridViewCustomers.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                DataGridViewButtonColumn detailsCol = new DataGridViewButtonColumn();
                detailsCol.Name = "Delete";
                detailsCol.Text = "Удалить запись";
                detailsCol.UseColumnTextForButtonValue = true;
                detailsCol.HeaderText = "";
                dataGridViewCustomers.Columns.Insert(dataGridViewCustomers.Columns.Count, detailsCol);
                DataGridViewButtonColumn detailsCol2 = new DataGridViewButtonColumn();
                detailsCol2.Name = "Print";
                detailsCol2.Text = "Распечатать повторно";
                detailsCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                detailsCol2.UseColumnTextForButtonValue = true;
                detailsCol2.HeaderText = "";
                dataGridViewCustomers.Columns.Insert(dataGridViewCustomers.Columns.Count, detailsCol2);
            }
        }
        private void dataGridViewCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dataGridViewCustomers.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = -1;
                    if (false == int.TryParse(dataGridViewCustomers.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                        if (int.TryParse(dataGridViewCustomers.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                        {
                        }                   
                    using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"]
                               .ConnectionString))
                    {
                        var customers = db.GetCollection<Customer>("customers");
                        customers.Delete(id);
                        var result = customers.Query().OrderBy(x => x.FIO).ToList();
                        dataGridViewCustomers.DataSource = result;
                        dataGridViewCustomers.Update();
                        dataGridViewCustomers.Refresh();
                        CustomerPrint = null;                       
                    }
                    this.LoadCustomers();
                }
            }
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dataGridViewCustomers.Columns[e.ColumnIndex].Name == "Print")
            {
                CustomerPrint = (Customer)dataGridViewCustomers.CurrentRow.DataBoundItem;
                this.Close();
            }
        }
     
        private void LogError(string msg)
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                // Get customer collection
                var col = db.GetCollection<Log>("log");

                Log newLog = new Log();
                newLog.TextLog = msg;
                col.Insert(newLog);
            }
        }  
        public Customer CustomerPrint { get; private set; }

        private void textBoxSearchFIO_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            SearchCustomer();
        }

        private void textBoxSearchFIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SearchCustomer();
            }
        }
    }
}
