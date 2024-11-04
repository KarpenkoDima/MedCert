using System;
using System.Windows.Forms;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.WinForms
{
    public partial class MedicalDoctor : BaseForm
    {
        /* ILogService _logService;
         IDoctorRepository _doctorRepository;
         IPrintService _printService;
         public MedicalDoctor(IDoctorRepository doctorRepository,
         ILogService logService,
         IPrintService printService)
         {
             _logService = logService;
             _printService = printService;
             _doctorRepository   = doctorRepository;

             InitializeComponent();
             LoadDB();
         }*/
        private readonly IDoctorRepository _doctorRepository;

        public MedicalDoctor(
            IDoctorRepository doctorRepository,
            ILogService logService,
            IPrintService printService)
            : base(logService, printService)
        {
            _doctorRepository = doctorRepository;
            InitializeComponent();
            LoadDB();
        }
        private void LoadDB()
        {
         
            var result = _doctorRepository.GetAll();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            // dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.DataSource = result;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Отчество";
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].HeaderText = "Фамилия";
            dataGridView1.Columns[4].HeaderText = "Ф.И.О.";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись о докторе", "Удалить запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = -1;
                    if (false == int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                        if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), out id))
                        {
                        }

                    MessageBox.Show(id.ToString());
                    _doctorRepository.Delete(id);
                    var result = _doctorRepository.GetAll();
                    dataGridView1.DataSource = result;
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                }
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dataGridView1.Rows[e.RowIndex].DataBoundItem is MDoctor doctor)
            {              
                _doctorRepository.Update(doctor);

                var result = _doctorRepository.GetAll();
                dataGridView1.DataSource = result;
                dataGridView1.Update();
                dataGridView1.Refresh();
            }
        }
        private void создатьToolStripButton_Click(object sender, EventArgs e)
        {
            AddDoctorForms addDoctorForms = new AddDoctorForms();
            addDoctorForms.ShowDialog();
            LoadDB();
        }
    }
}