using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EasyDox;
using LiteDB;
using System.Configuration;
using WindowsFormsApp1.Classes;
using WindowsFormsApp1.WinForms;
using System.Configuration;
using static System.Windows.Forms.AxHost;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        BindingSource binding = new BindingSource();
        public Form1()
        {
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
                PrintCert();
                #region /* ------------ Сохранение клиента в БД ---------- */
                SaveCustomer();
                #endregion /* ------------------------------------ */
            }           
        }
        private void PrintCert()
        {
            string medicalDoctors = GetMedDoctorsInString();

            /*******************************************/
            string BoD = maskedTextBoxBoD.Text.Length == 10 ? maskedTextBoxBoD.Text : new string(' ', 10);
            string dateCheck = maskedTextBoxDateCheck.Text.Length == 10 ? maskedTextBoxDateCheck.Text : new string(' ', 10);
            string timeCheck = maskedTextBoxTimeCheck.Text.Length == 5 ? maskedTextBoxTimeCheck.Text : new string(' ', 5);
            /*******************************************/

            var fieldValues = new Dictionary<string, string>
            {
                        {"D1", dateCheck[0].ToString()},
                        {"D2", dateCheck[1].ToString()},
                        {"M1", dateCheck[3].ToString()},
                        {"M2", dateCheck[4].ToString()},
                        {"Y1", dateCheck[6].ToString()},
                        {"Y2", dateCheck[7].ToString()},
                        {"Y3", dateCheck[8].ToString()},
                        {"Y4", dateCheck[9].ToString()},
                        {"H1", timeCheck[0].ToString()},
                        {"H2", timeCheck[1].ToString()},
                        {"m1", timeCheck[3].ToString()},
                        {"m2", timeCheck[4].ToString()},
                        {"FIO", textBoxFIO.Text},
                        {"D3", BoD[0].ToString()},
                        {"D4", BoD[1].ToString()},
                        {"M3", BoD[3].ToString()},
                        {"M4", BoD[4].ToString()},
                        {"Y5", BoD[6].ToString()},
                        {"Y6", BoD[7].ToString()},
                        {"Y7", BoD[8].ToString()},
                        {"Y8", BoD[9].ToString()},
                        {"S", comboBoxSex.SelectedItem != null ? comboBoxSex.SelectedItem.ToString() : ""},
                        {"STREET", textBoxAddress.Text},
                        {
                            "WORK1",
                            textBoxResultMedCheck.Text
                        },
                        {"R1", (comboBoxResultMedCheck.SelectedItem != null) ? comboBoxResultMedCheck.SelectedItem.ToString(): ""},
                        {
                            "WORK2",
                            textBoxMedExam.Text
                        },
                        {"R2", (comboBoxMedExam.SelectedItem != null) ?  comboBoxMedExam.SelectedItem.ToString() : ""},
                        {"FIODOCTOR", medicalDoctors}
                    };
            string dir = Application.StartupPath;
            try
            {
                string path = Path.Combine(dir + ConfigurationManager.AppSettings["templateDocx"]);
                string pathOut = Path.Combine(dir + ConfigurationManager.AppSettings["outputDoc"]);
                var engine = new Engine();
                engine.Merge(path, fieldValues, pathOut);
                System.Diagnostics.Process.Start(pathOut);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при попытке распечатать сертификат. Возможно открыт файл с сертификатом {Environment.NewLine} Лучше перезапустите программу.",
                    "Ошибка при печати сертификата",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LogError(ex.Message);
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
            if (textBoxFIO.DataBindings.Count <= 0)
            {
                try
                {
                    using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
                    {
                        // Get customer collection
                        var col = db.GetCollection<Customer>("customers");
                        // Create unique index in Name field                    
                        // Insert new customer document (Id will be auto-incremented)
                        var customer = GetCustomer();                        
                        col.Insert(customer);

                        ClearDataBindings();
                    }
                }
                catch (Exception ex)
                {

                    LogError(ex.Message);
                }
            }
            /* else
             {
                 try
                 {
                     using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
                     {
                         // Get customer collection
                         var col = db.GetCollection<Customer>("customers");
                         var updateCustomer = (Customer)textBoxFIO.DataBindings[0].DataSource;
                         col.Update(updateCustomer);
                         updateCustomer = null;
                         ClearDataBindings();
                     }
                 }
                 catch (Exception ex)
                 {

                     LogError(ex.Message);
                 }
            }*/
        }
        private void LoadMD()
        {
            using (var db = new LiteDatabase(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                //if (db.CollectionExists("MDoctors")) db.DropCollection("MDoctors");
                var doctors = db.GetCollection<Classes.MDoctor>("MDoctors");
                var result = doctors.Query().OrderBy(x => x.LastName).ToList();
                checkedListBoxMD.DataSource = result;
                checkedListBoxMD.DisplayMember = "FullName";
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
        private void buttonMD_Click(object sender, EventArgs e)
        {
            MedicalDoctor medicalDoctor = new MedicalDoctor();
            medicalDoctor.ShowDialog();
            LoadMD();
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
            CustomersForm customersForm = new CustomersForm();
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
            customersForm.Close();
            customersForm.Dispose();
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
            LogForm logForm = new LogForm();
            logForm.ShowDialog();

            logForm.Close();
            logForm.Dispose();
        }
    }
}