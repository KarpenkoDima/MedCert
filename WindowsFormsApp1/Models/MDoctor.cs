using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class MDoctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TypeMD { get; set; }

        public string FullName
        {
            get
            {
                return this.LastName + " " + FirstName + " " + MiddleName;
            }
        }
    }
}
