using System;
using System.Configuration;
using System.Windows.Forms;
using LiteDB;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.WinForms
{
    public partial class AddTextTemplateForm : Form
    {
        // fix/add-doctor-form-di
        private readonly ITextTemplateRepository _textTemplateRepository;
        
        public AddTextTemplateForm(ITextTemplateRepository textTemplateRepository)
        {
            _textTemplateRepository = textTemplateRepository ?? throw new ArgumentNullException(nameof(textTemplateRepository));
            InitializeComponent();
            comboBoxCategory.Items.AddRange(TextTemplate.Categories);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveTemplate())
            {
                this.Close();
                this.Dispose();
            }
        }

        private bool SaveTemplate()
        {
            IsEmptyTexBoxes();
            if (GetErrorProvider()) return false;

            var template = new TextTemplate
            {
                Category = comboBoxCategory.SelectedItem?.ToString(),
                Text = textBoxText.Text,
            };
            try
            {
                _textTemplateRepository.Add(template);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить шаблон.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void IsEmptyTexBoxes()
        {
            if (comboBoxCategory.SelectedIndex == -1)
            {
                errorProviderAddTemplate.SetError(comboBoxCategory, "Выберите категорию");
            }
            else
            {
                errorProviderAddTemplate.SetError(comboBoxCategory, string.Empty);
            }

            if (textBoxText.Text.Length == 0)
            {
                errorProviderAddTemplate.SetError(textBoxText, "Поле не может быть пустым. Введите текст шаблона.");
            }
            else
            {
                errorProviderAddTemplate.SetError(textBoxText, string.Empty);
            }
        }
        private bool GetErrorProvider()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (errorProviderAddTemplate.GetError(control) != String.Empty) return true;
            }
            return false;
        }
    }
}