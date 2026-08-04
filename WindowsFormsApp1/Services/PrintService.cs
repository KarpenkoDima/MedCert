using EasyDox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public interface IPrintService
    {
        bool PrintCertificate(PrintCertificateData data, out string error);
    }
    public class PrintService:IPrintService
    {
        private readonly string _templatePath;
        private readonly string _outputPath;
        private readonly ILogService _logService;

        public PrintService(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _templatePath = ResolveApplicationPath("templateDocx");
            _outputPath = ResolveApplicationPath("outputDoc");
        }

        public bool PrintCertificate(PrintCertificateData data, out string error)
        {
            error = null;
            try
            {
                if (!File.Exists(_templatePath))
                {
                    error = $"Не найден шаблон сертификата: {_templatePath}";
                    _logService.LogError(error);
                    return false;
                }

                // Проверяем, не заблокирован ли выходной файл
                if (IsFileLocked(_outputPath))
                {
                    error = "Файл сертификата заблокирован другим процессом.\n" +
                           "Пожалуйста, закройте ранее открытые сертификаты и попробуйте снова.";
                    _logService.LogWarning("Попытка печати заблокированного файла: " + _outputPath);
                    return false;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(_outputPath));
                var fieldValues = CreateFieldValues(data);
                var engine = new Engine();
                engine.Merge(_templatePath, fieldValues, _outputPath);

                using (var process = Process.Start(new ProcessStartInfo
                {
                    FileName = _outputPath,
                    UseShellExecute = true
                }))
                {
                    if (process == null)
                    {
                        throw new InvalidOperationException("Не удалось открыть сформированный сертификат в Word.");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                error = "Произошла ошибка при попытке распечатать сертификат. " +
                       "Возможно открыт файл с сертификатом\nЛучше перезапустите программу.";

                _logService.LogError("Ошибка формирования или открытия сертификата", ex);
                return false;
            }
        }

        private static string ResolveApplicationPath(string settingName)
        {
            var configuredPath = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrWhiteSpace(configuredPath))
            {
                throw new ConfigurationErrorsException(
                    $"В App.config отсутствует настройка '{settingName}'.");
            }

            var relativePath = configuredPath.TrimStart(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar);

            return Path.Combine(Application.StartupPath, relativePath);
        }

        /// <summary>
        /// Проверяет, заблокирован ли файл другим процессом
        /// </summary>
        private bool IsFileLocked(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                using (File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                }

                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        private Dictionary<string, string> CreateFieldValues(PrintCertificateData data)
        {
            return new Dictionary<string, string>
        {
            // Дата осмотра
            {"D1", data.CheckDate[0].ToString()},
            {"D2", data.CheckDate[1].ToString()},
            {"M1", data.CheckDate[3].ToString()},
            {"M2", data.CheckDate[4].ToString()},
            {"Y1", data.CheckDate[6].ToString()},
            {"Y2", data.CheckDate[7].ToString()},
            {"Y3", data.CheckDate[8].ToString()},
            {"Y4", data.CheckDate[9].ToString()},

            // Время
            {"H1", data.CheckTime[0].ToString()},
            {"H2", data.CheckTime[1].ToString()},
            {"m1", data.CheckTime[3].ToString()},
            {"m2", data.CheckTime[4].ToString()},

            // ФИО и адрес
            {"FIO", data.FullName},
            {"STREET", data.Address},

            // Дата рождения
            {"D3", data.BirthDate[0].ToString()},
            {"D4", data.BirthDate[1].ToString()},
            {"M3", data.BirthDate[3].ToString()},
            {"M4", data.BirthDate[4].ToString()},
            {"Y5", data.BirthDate[6].ToString()},
            {"Y6", data.BirthDate[7].ToString()},
            {"Y7", data.BirthDate[8].ToString()},
            {"Y8", data.BirthDate[9].ToString()},

            // Другие поля
            {"S", data.Sex},
            {"WORK1", data.MedCheckResult},
            {"R1", data.R1Result},
            {"WORK2", data.MedExamResult},
            {"R2", data.R2Result},
            {"FIODOCTOR", data.DoctorsString}
        };
        }
    }
}
