using System;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.WinForms
{
    public partial class TextTemplateForm : BaseForm
    {
        private readonly ITextTemplateRepository _textTemplateRepository;
        private readonly IFormFactory _formFactory;
        public TextTemplateForm(
            ITextTemplateRepository textTemplateRepository,
            ILogService logService, 
            IPrintService printService,
            IFormFactory formFactory)          
            : base(logService, printService)
        {
            
            _textTemplateRepository = textTemplateRepository;
            _formFactory = formFactory;

            InitializeComponent();
            LoadDB();
        }

        private void LoadDB()
        {

            var result = _textTemplateRepository.GetAll();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            // dataGridView1.AutoGenerateColumns = true;
            // dataGridView1.AllowUserToAddRows = true;
            dataGridView1.DataSource = result;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns.Remove("Category");
            var categoryColumn = new DataGridViewComboBoxColumn
            {
                Name = "Category",
                DataPropertyName = "Category",
                HeaderText = "Категория"                
            };
            categoryColumn.Items.AddRange(TextTemplate.Categories);
            dataGridView1.Columns.Insert(1, categoryColumn);

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
                if (MessageBox.Show("Вы действительно хотите удалить запись", "Удалить запись", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = -1;
                    if (false == int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                        if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), out id))
                        {
                        }

                    MessageBox.Show(id.ToString());
                    _textTemplateRepository.Delete(id);
                    var result = _textTemplateRepository.GetAll();
                    dataGridView1.DataSource = result;
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {            
            if (!(dataGridView1.Rows[e.RowIndex].DataBoundItem is TextTemplate template)) return;
            if (template.Id == 0)
                return;
            else
                _textTemplateRepository.Update(template);
        }
        private void создатьToolStripButton_Click(object sender, EventArgs e)
        {
            using (var textTemplateForm = _formFactory.Create<AddTextTemplateForm>())
            {
                textTemplateForm.ShowDialog();
            }
            LoadDB();
        }
    }
}