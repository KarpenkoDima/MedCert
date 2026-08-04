using System;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsApp1.WinForms;
using System.Linq;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Data.Repositories;

namespace WindowsFormsApp1
{
    public partial class Medcert : Form
    {
        private static readonly string[] SupportedDateFormats =
        {
            "dd.MM.yyyy",
            "dd/MM/yyyy",
            "dd-MM-yyyy",
            "dd MM yyyy"
        };

        private const string SupportedTimeFormat = @"hh\:mm";

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
            ValidateForm();
            if (HasValidationErrors())
            {
                return;
            }

            if (!TryCreateCustomer(out var customer))
            {
                MessageBox.Show(
                    "Не удалось обработать дату или время. Проверьте введённые значения.",
                    "Некорректные данные",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var isRepeatPrint = textBoxFIO.DataBindings.Count > 0;
            if (!PrintCertificate(customer))
            {
                return;
            }

            if (!ConfirmSuccessfulPrint())
            {
                _logService.LogWarning(
                    "Сертификат сформирован, но оператор не подтвердил успешную печать");
                return;
            }

            if (!isRepeatPrint)
            {
                SaveCustomer(customer);
            }
            else
            {
                _logService.LogInfo("Оператор подтвердил повторную печать сертификата");
            }
        }

        private bool ConfirmSuccessfulPrint()
        {
            return MessageBox.Show(
                "Документ открыт в Word.\n\n" +
                "1. Проверьте заполнение сертификата.\n" +
                "2. Выполните печать на бланк.\n" +
                "3. После успешной печати вернитесь в программу и нажмите «Да».\n\n" +
                "Сертификат успешно распечатан?",
                "Подтверждение печати",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private bool PrintCertificate(Customer customer)
        {
            var data = new PrintCertificateData(
                customer.MedDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                customer.Time.ToString("HH:mm", CultureInfo.InvariantCulture),
                customer.FIO,
                customer.BoD.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                customer.Sex.ToString(CultureInfo.InvariantCulture),
                customer.Registration,
                customer.MedCheck,
                customer.R1.ToString(CultureInfo.InvariantCulture),
                customer.MedAnalisys,
                customer.R2.ToString(CultureInfo.InvariantCulture),
                customer.MedDoctors
            );
          
            if (!_printService.PrintCertificate(data, out string error))
            {
                MessageBox.Show(error,
                    "Ошибка при печати сертификата",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
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
        private bool SaveCustomer(Customer customer)
        {
            try
            {
                _customerRepository.Add(customer);
                return true;
            }
            catch (Exception ex)
            {
                _logService.LogError("Сертификат сформирован, но запись не сохранена", ex);
                MessageBox.Show(
                    "Сертификат открыт в Word, но запись о нём не удалось сохранить в базе данных.",
                    "Ошибка сохранения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        private void LoadMD()
        {           
            
            var result =  _doctorRepository.GetAll();            
            checkedListBoxMD.DataSource = result;
            checkedListBoxMD.DisplayMember = "FullName";
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
        private bool TryCreateCustomer(out Customer customer)
        {
            customer = null;

            if (!TryParseDate(maskedTextBoxBoD.Text, out var birthDate) ||
                !TryParseDate(maskedTextBoxDateCheck.Text, out var medicalDate) ||
                !TimeSpan.TryParseExact(
                    maskedTextBoxTimeCheck.Text,
                    SupportedTimeFormat,
                    CultureInfo.InvariantCulture,
                    out var medicalTime))
            {
                return false;
            }

            customer = new Customer
            {
                FIO = textBoxFIO.Text.Trim(),
                BoD = birthDate,
                MedDate = medicalDate,
                Time = medicalDate.Date.Add(medicalTime),
                Registration = textBoxAddress.Text.Trim(),
                MedCheck = textBoxResultMedCheck.Text,
                MedAnalisys = textBoxMedExam.Text,
                R1 = comboBoxResultMedCheck.SelectedIndex + 1,
                R2 = comboBoxMedExam.SelectedIndex + 1,
                Sex = comboBoxSex.SelectedIndex == -1 ? 3 : comboBoxSex.SelectedIndex + 1,
                MedDoctors = GetMedDoctorsInString()
            };

            return true;
        }

        private static bool TryParseDate(string value, out DateTime date)
        {
            return DateTime.TryParseExact(
                value,
                SupportedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
        }

        private bool HasValidationErrors()
        {
            foreach (Control control in groupBoxForm.Controls)
            {
                if (errorProvider1.GetError(control) != String.Empty) return true;
            }
            return false;
        }

        private void ValidateForm()
        {
            errorProvider1.SetError(
                maskedTextBoxDateCheck,
                TryParseDate(maskedTextBoxDateCheck.Text, out _)
                    ? string.Empty
                    : "Укажите корректную дату осмотра");

            errorProvider1.SetError(
                maskedTextBoxBoD,
                TryParseDate(maskedTextBoxBoD.Text, out _)
                    ? string.Empty
                    : "Укажите корректную дату рождения");

            errorProvider1.SetError(
                maskedTextBoxTimeCheck,
                TimeSpan.TryParseExact(
                    maskedTextBoxTimeCheck.Text,
                    SupportedTimeFormat,
                    CultureInfo.InvariantCulture,
                    out _)
                    ? string.Empty
                    : "Укажите корректное время от 00:00 до 23:59");

            errorProvider1.SetError(
                textBoxFIO,
                string.IsNullOrWhiteSpace(textBoxFIO.Text)
                    ? "Укажите Ф.И.О."
                    : string.Empty);

            errorProvider1.SetError(
                textBoxAddress,
                string.IsNullOrWhiteSpace(textBoxAddress.Text)
                    ? "Укажите место проживания"
                    : string.Empty);

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
