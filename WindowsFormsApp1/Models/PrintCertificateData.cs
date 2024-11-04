using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class PrintCertificateData
    {
        // Models/PrintCertificateData.cs

        public string CheckDate { get; }
        public string CheckTime { get; }
        public string FullName { get; }
        public string BirthDate { get; }
        public string Sex { get; }
        public string Address { get; }
        public string MedCheckResult { get; }
        public string R1Result { get; }
        public string MedExamResult { get; }
        public string R2Result { get; }
        public string DoctorsString { get; }

        public PrintCertificateData(
            string checkDate,
            string checkTime,
            string fullName,
            string birthDate,
            string sex,
            string address,
            string medCheckResult,
            string r1Result,
            string medExamResult,
            string r2Result,
            string doctorsString)
        {
            CheckDate = NormalizeDate(checkDate);
            CheckTime = NormalizeTime(checkTime);
            FullName = fullName;
            BirthDate = NormalizeDate(birthDate);
            Sex = sex;
            Address = address;
            MedCheckResult = medCheckResult;
            R1Result = r1Result;
            MedExamResult = medExamResult;
            R2Result = r2Result;
            DoctorsString = doctorsString;
        }

        private string NormalizeDate(string date)
            => date.Length == 10 ? date : new string(' ', 10);

        private string NormalizeTime(string time)
            => time.Length == 5 ? time : new string(' ', 5);
    }
}
