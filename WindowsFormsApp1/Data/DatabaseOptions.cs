using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public bool EnableLogging { get; set; }
        public int CommandTimeout { get; set; }
        public bool EnableCaching { get; set; }
        public int CacheTimeout { get; set; }
    }
}
