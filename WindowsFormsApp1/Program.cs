using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.WinForms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool prevInstance;
            _ = new System.Threading.Mutex(true, Process.GetCurrentProcess().ProcessName, out prevInstance);
            if (prevInstance == false)
            {
                MessageBox.Show("Приложение уже запущено");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            var databaseOptions = CreateDatabaseOptions();
            services.AddSingleton(databaseOptions);

            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Medcert>();
                Application.Run(form1);
            }
        }
        private static DatabaseOptions CreateDatabaseOptions()
        {
            // Используем явное указание System.Configuration
            return new DatabaseOptions
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString,
                EnableLogging = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableLogging"] ?? "false"),
                CommandTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"] ?? "30"),
                EnableCaching = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableCaching"] ?? "false"),
                CacheTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CacheTimeout"] ?? "300")
            };
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Репозитории
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            // Сервисы
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IPrintService, PrintService>();            

            // Фабрика форм
            services.AddSingleton<IFormFactory, FormFactory>();

            // Формы
            services.AddTransient<Medcert>();
            services.AddTransient<CustomersForm>();
            services.AddTransient<MedicalDoctor>();
            services.AddTransient<LogForm>();
        }
    }
}