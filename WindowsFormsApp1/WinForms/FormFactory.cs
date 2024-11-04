using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.WinForms
{
    public interface IFormFactory
    {
        T Create<T>() where T : Form;
    }

    public class FormFactory : IFormFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FormFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Create<T>() where T : Form
        {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider);
        }
    }
}
