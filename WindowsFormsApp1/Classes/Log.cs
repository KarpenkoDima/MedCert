using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Classes
{
    public class Log
    {
        public int Id { get; set; }
        public String TextLog { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
