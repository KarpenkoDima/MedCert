using System.Windows.Forms;

namespace WindowsFormsApp1.Services
{
    public interface IPrintConfirmationService
    {
        bool ConfirmSuccessfulPrint();
    }

    public sealed class PrintConfirmationService : IPrintConfirmationService
    {
        public bool ConfirmSuccessfulPrint()
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
    }
}
