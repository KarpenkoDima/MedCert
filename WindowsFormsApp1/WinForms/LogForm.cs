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

namespace WindowsFormsApp1.WinForms
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            /********* My code ***********/
            LoadDB();
        }
        private void LoadDB()
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                //if (db.CollectionExists("MDoctors")) db.DropCollection("MDoctors");
                var doctors = db.GetCollection<Classes.Log>("Log");

                var result = doctors.Query().ToList();
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                // dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.DataSource = result;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].HeaderText = "Id";
                dataGridView1.Columns[1].HeaderText = "Текст сообщения";
                dataGridView1.Columns[2].HeaderText = "Время записи";

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                DataGridViewButtonColumn detailsCol = new DataGridViewButtonColumn();
                detailsCol.Name = "Delete";
                detailsCol.Text = "Удалить запись";
                detailsCol.UseColumnTextForButtonValue = true;
                detailsCol.HeaderText = "";
                dataGridView1.Columns.Insert(dataGridView1.Columns.Count, detailsCol);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = -1;
                    if (false == int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out id))
                    {
                    }

                    MessageBox.Show(id.ToString());
                    using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"]
                               .ConnectionString))
                    {
                        var log = db.GetCollection<Classes.Log>("log");
                        log.Delete(id);
                        var result = log.Query().OrderBy(x => x.Id).ToList();
                        dataGridView1.DataSource = result;
                        dataGridView1.Update();
                        dataGridView1.Refresh();
                    }
                }
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"]
                              .ConnectionString))
                {
                    var log = db.GetCollection<Classes.Log>("log");
                    log.DeleteAll();
                    var result = log.Query().OrderBy(x => x.Id).ToList();
                    dataGridView1.DataSource = result;
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                }
            }
        }
    }
}
