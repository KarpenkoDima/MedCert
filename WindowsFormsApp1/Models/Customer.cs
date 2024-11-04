using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime BoD { get; set; }
        public DateTime MedDate { get; set; }
        public DateTime Time { get; set; }
        public int Sex { get; set; }
        public string Registration { get; set; }
        public string MedCheck { get; set; }
        public string MedAnalisys { get; set; }
        public int R1 { get; set; }
        public int R2 { get; set; }
        public string MedDoctors { get; set; }
        public string Index => string.Join("; ", FIO, Registration, BoD.Year.ToString(), DateTime.Now);
    }
}
