using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LiteDB;
using WindowsFormsApp1.WinForms;
using System.Linq;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Data.Repositories;

namespace WindowsFormsApp1
{
    public partial class Medcert : Form
    {
        BindingSource binding = new BindingSource();


        private readonly IFormFactory _formFactory;

       private readonly IPrintService _printService;
        private readonly ILogService _logService;
        ICustomerRepository _customerRepository;
        IDoctorRepository _doctorRepository;
        public Medcert(IPrintService printService,
        ILogService logService,
        ICustomerRepository customerRepository,
        IDoctorRepository doctorRepository,
        IFormFactory formFactory)
        {
            _printService = printService;
            _logService = logService;
            _customerRepository = customerRepository;
            _doctorRepository = doctorRepository;
            _formFactory = formFactory;

            InitializeComponent();

            int maxWidth = 0, temp = 0;
            foreach (var item in comboBoxSex.Items)
            {
                temp = TextRenderer.MeasureText(item.ToString(), comboBoxSex.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            comboBoxSex.DropDownWidth = maxWidth;
            LoadMD();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckForEerror();
            if (false == GetErrorProvider())
            {
                PrintCertificate();
                #region /* ------------ Сохранение клиента в БД ---------- */
                SaveCustomer();
                #endregion /* ------------------------------------ */
            }           
        }
        private void PrintCertificate()
        {
            var data = new PrintCertificateData(
                maskedTextBoxDateCheck.Text,
                maskedTextBoxTimeCheck.Text,
                textBoxFIO.Text,
                maskedTextBoxBoD.Text,
                comboBoxSex.SelectedItem?.ToString() ?? "",
                textBoxAddress.Text,
                textBoxResultMedCheck.Text,
                comboBoxResultMedCheck.SelectedItem?.ToString() ?? "",
                textBoxMedExam.Text,
                comboBoxMedExam.SelectedItem?.ToString() ?? "",
                GetMedDoctorsInString()
            );
          
            if (!_printService.PrintCertificate(data, out string error))
            {
                MessageBox.Show(error,
                    "Ошибка при печати сертификата",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _logService.LogError(error);
            }
        }
    
        private string GetMedDoctorsInString()
        {
            string medicalDoctors = String.Empty;
            int i = 0;
            if (checkedListBoxMD.CheckedItems.Count > 0)
            {
                if (textBoxMDFIO.Text.Length > 0) medicalDoctors = textBoxMDFIO.Text + ", ";
                for (; i < checkedListBoxMD.CheckedItems.Count - 1; i++)
                {
                    medicalDoctors += (checkedListBoxMD.CheckedItems[i] as MDoctor)?.FullName + ", ";
                }
            }
            else medicalDoctors = textBoxMDFIO.Text;

            if (checkedListBoxMD.CheckedIndices.Count > 0) medicalDoctors += (checkedListBoxMD.CheckedItems[i] as MDoctor)?.FullName;

            return medicalDoctors;
        }
        private void SaveCustomer()
        {
            ClearDataBindings();
            if (textBoxFIO.DataBindings.Count <= 0)
            {
                try
                {
                   
                    _customerRepository.Add(GetCustomer());
                    ClearDataBindings();

                }
                catch (Exception ex)
                {
                    LogError(ex.Message);
                }
            }           
        }
        private void LoadMD()
        {           
            
            var result =  _doctorRepository.GetAll();            
            checkedListBoxMD.DataSource = result;
            checkedListBoxMD.DisplayMember = "FullName";
        }
        private void LogError(string msg)
        {            
            _logService.LogError(msg);
        }
        private void buttonMD_Click(object sender, EventArgs e)
        {
            using (var medicalDoctor = _formFactory.Create<MedicalDoctor>())
            {
                if (medicalDoctor.ShowDialog() == DialogResult.OK)
                {
                    LoadMD();
                }
            }
        }
        private Customer GetCustomer()
        {
            var customer = new Customer
            {
                FIO = textBoxFIO.Text,
                BoD = DateTime.Parse(string.Join(".", maskedTextBoxBoD.Text)),
                MedDate = DateTime.Parse(string.Join(".", maskedTextBoxDateCheck.Text)),
                Time = DateTime.Parse(string.Join(":", maskedTextBoxTimeCheck.Text)),
                Registration = textBoxAddress.Text,
                MedCheck = textBoxResultMedCheck.Text,
                MedAnalisys = textBoxMedExam.Text,
                R1 = comboBoxResultMedCheck.SelectedIndex + 1,
                R2 = comboBoxMedExam.SelectedIndex + 1,
                Sex = comboBoxSex.SelectedIndex == -1 ? 3 : comboBoxSex.SelectedIndex + 1,
                MedDoctors = GetMedDoctorsInString()
            };
            return customer;
        }
        private bool GetErrorProvider()
        {
            foreach (Control control in groupBoxForm.Controls)
            {
                if (errorProvider1.GetError(control) != String.Empty) return true;
            }
            return false;
        }

        private void CheckForEerror()
        {
            Regex regex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[.|\s|-|/](0[1-9]|1[012])[.|\s|-|для проверки коректности дат](19|20)\d\d$");
            if (false == regex.Match(this.maskedTextBoxDateCheck.Text).Success)
            {
                errorProvider1.SetError(this.maskedTextBoxDateCheck, "Неверная дата");
            }
            else
            {
                errorProvider1.SetError(this.maskedTextBoxDateCheck, string.Empty);
            }

            if (false == regex.Match(this.maskedTextBoxBoD.Text).Success)
            {
                errorProvider1.SetError(this.maskedTextBoxBoD, "Неверная дата");
            }
            else
            {
                errorProvider1.SetError(this.maskedTextBoxBoD, string.Empty);
            }
            regex = new Regex(@"^([0-5]?\d):([0-5]?\d)$");
            if (false == regex.Match(this.maskedTextBoxTimeCheck.Text).Success)
            {
                errorProvider1.SetError(this.maskedTextBoxTimeCheck, "Неверено указано время");
            }
            else
            {
                errorProvider1.SetError(this.maskedTextBoxTimeCheck, string.Empty);
            }

            if (textBoxMDFIO.Text.Length <= 0 && checkedListBoxMD.CheckedItems.Count <= 0)
            {
                errorProvider1.SetError(this.checkedListBoxMD, "Укажите/выберите доктора");
            }
            else
            {
                errorProvider1.SetError(this.checkedListBoxMD, String.Empty);
            }

            if (comboBoxSex.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBoxSex, "Не выбран пол");
            }
            else
            {
                errorProvider1.SetError(comboBoxSex, String.Empty);
            }

            if (comboBoxMedExam.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBoxMedExam, "Выберите значение");
            }
            else
            {
                errorProvider1.SetError(comboBoxMedExam, String.Empty);
            }
            if (comboBoxResultMedCheck.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBoxResultMedCheck, "Выберите значение");
            }
            else
            {
                errorProvider1.SetError(comboBoxResultMedCheck, String.Empty);
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                textBoxResultMedCheck.Text = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (var control in groupBoxForm.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)control).Clear();
                }
                else if (control is CheckedListBox)
                {
                    for (int i = 0; i < checkedListBoxMD.Items.Count; i++)
                    {
                        checkedListBoxMD.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
                else if (control is System.Windows.Forms.ComboBox)
                {
                    ((System.Windows.Forms.ComboBox)control).SelectedIndex = -1;

                }
                else if (control is MaskedTextBox)
                {
                    ((MaskedTextBox)control).Clear();
                }
            }
            ClearDataBindings();
        }

        private void ListCustomerCert_Click(object sender, EventArgs e)
        {
            using (var customersForm = _formFactory.Create<CustomersForm>())
            {
                customersForm.ShowDialog();
                var customer = customersForm.CustomerPrint;
                if (customer != null)
                {
                    RepeatCustomerPrint(customer);
                }
                else
                {
                    ClearDataBindings();
                }
            }          
        }
        private void RepeatCustomerPrint(Customer customer)
        {

            binding.DataSource = customer;
            ClearDataBindings();
            textBoxFIO.DataBindings.Add("Text", customer, "FIO");

            maskedTextBoxBoD.DataBindings.Add("Text", customer, "BoD");

            maskedTextBoxDateCheck.DataBindings.Add("Text", customer, "MedDate");

            maskedTextBoxTimeCheck.DataBindings.Add("Text", customer, "Time.TimeOfDay", true, DataSourceUpdateMode.OnPropertyChanged);

            textBoxAddress.DataBindings.Add("Text", customer, "Registration");

            textBoxMedExam.DataBindings.Add("Text", customer, "MedAnalisys");

            textBoxResultMedCheck.DataBindings.Add("Text", customer, "MedCheck");

            comboBoxResultMedCheck.DataBindings.Add("Text", customer, "R1");

            comboBoxMedExam.DataBindings.Add("Text", customer, "R2");

            comboBoxSex.DataBindings.Add("Text", customer, "Sex");
            


            string medicalDoctors;
            var doctors = customer.MedDoctors.Split(',').ToList();           
            for (int j = 0; j < doctors.Count; j++)
            {
                for (int i = 0; i <= checkedListBoxMD.Items.Count - 1; i++)
                {
                    medicalDoctors = (checkedListBoxMD.Items[i] as MDoctor)?.FullName;
                    
                    if (medicalDoctors.Trim().Contains(doctors[j].Trim()))
                    {
                        checkedListBoxMD.SetItemChecked(i, true);
                        doctors.RemoveAt(j);
                        j = -1;
                        break;
                    }
                }
            }
            if (doctors.Count > 0)
            {
                customer.MedDoctors = "";
                foreach (var item in doctors)
                {
                    customer.MedDoctors += item;
                }
                    textBoxMDFIO.DataBindings.Add("Text", customer, "MedDoctors");
                
            }
        }

        private void ClearDataBindings()
        {
            textBoxFIO.DataBindings.Clear();
            maskedTextBoxBoD.DataBindings.Clear();
            maskedTextBoxDateCheck.DataBindings.Clear();
            maskedTextBoxTimeCheck.DataBindings.Clear();
            textBoxAddress.DataBindings.Clear();
            textBoxMedExam.DataBindings.Clear();
            textBoxResultMedCheck.DataBindings.Clear();
            comboBoxResultMedCheck.DataBindings.Clear();
            comboBoxMedExam.DataBindings.Clear();
            comboBoxSex.DataBindings.Clear();
            textBoxMDFIO.DataBindings.Clear();
        }

        private void Log_Click(object sender, EventArgs e)
        {

            using (var customersForm = _formFactory.Create<LogForm>())
            {
                if (customersForm.ShowDialog() == DialogResult.OK)
                {


                }
            }
        }                   
    }
}