using System;
using System.Configuration;
using System.Windows.Forms;
using LiteDB;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.WinForms
{
    public partial class AddDoctorForms : Form
    {
        // fix/add-doctor-form-di
        private readonly IDoctorRepository _doctorRepository;
        
        public AddDoctorForms(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveMD();
            this.Close();
            this.Dispose();
        }

        private void SaveMD()
        {
            IsEmptyTexBoxes();
            if (false == GetErrorProvider())
            {
                var doctor = new MDoctor()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    MiddleName = textBoxMiddleName.Text,
                    TypeMD = textBoxPosition.Text
                };
                try
                {
                    _doctorRepository.Add(doctor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить врача.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void IsEmptyTexBoxes()
        {
            if (textBoxFirstName.Text.Length == 0)
            {
                errorProviderAddDoctor.SetError(textBoxFirstName, "Поле не может быть пустым. Введите имя врача.");
            }
            else
            {
                errorProviderAddDoctor.SetError(textBoxFirstName, String.Empty);
            }

            if (textBoxLastName.Text.Length == 0)
            {
                errorProviderAddDoctor.SetError(textBoxLastName, "Поле не может быть пустым. Введите фыамилию врача.");
            }
            else
            {
                errorProviderAddDoctor.SetError(textBoxLastName, String.Empty);
            }
        }
        private bool GetErrorProvider()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (errorProviderAddDoctor.GetError(control) != String.Empty) return true;
            }
            return false;
        }
    }
}