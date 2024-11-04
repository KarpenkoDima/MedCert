using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.WinForms
{
    public abstract class BaseForm : Form
    {
        protected readonly ILogService LogService;
        protected readonly IPrintService PrintService;

        protected BaseForm(
            ILogService logService,
            IPrintService printService)
        {
            LogService = logService;
            PrintService = printService;
        }

        protected void HandleError(Exception ex)
        {
            LogService.LogError(ex.Message, ex);
            MessageBox.Show(
                "An error occurred. Please try again.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
