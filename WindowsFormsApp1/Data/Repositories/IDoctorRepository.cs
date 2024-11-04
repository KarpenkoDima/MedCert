using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data.Repositories
{
    public interface IDoctorRepository : IRepository<MDoctor>
    {
        // Специфичные для докторов методы могут быть добавлены здесь
    }
}
