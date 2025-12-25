using EasyDox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private ILogService _logService;

        public PrintService(ILogService logService)
        {
            var dir = Application.StartupPath;
            _templatePath = Path.Combine(dir + ConfigurationManager.AppSettings["templateDocx"]);
            _outputPath = Path.Combine(dir + ConfigurationManager.AppSettings["outputDoc"]);
           _logService = logService;
        }

        public bool PrintCertificate(PrintCertificateData data, out string error)
        {
            error = null;
            try
            {
                // Проверяем, не заблокирован ли выходной файл
                if (IsFileLocked(_outputPath))
                {
                    error = "Файл сертификата заблокирован другим процессом.\n" +
                           "Пожалуйста, закройте ранее открытые сертификаты и попробуйте снова.";
                    _logService.LogWarning("Попытка печати заблокированного файла: " + _outputPath);

                    // Попытка закрыть процесс Word, удерживающий файл
                    if (TryCloseWordProcess(_outputPath))
                    {
                        System.Threading.Thread.Sleep(500); // Даем время на закрытие
                        if (!IsFileLocked(_outputPath))
                        {
                            _logService.LogInfo("Файл успешно разблокирован");
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                var fieldValues = CreateFieldValues(data);
                var engine = new Engine();
                engine.Merge(_templatePath, fieldValues, _outputPath);
                System.Diagnostics.Process.Start(_outputPath);
                return true;
            }
            catch (Exception ex)
            {
                error = "Произошла ошибка при попытке распечатать сертификат. " +
                       "Возможно открыт файл с сертификатом\nЛучше перезапустите программу.";

                _logService.LogError($"Ошибка печати: {ex.Message}");
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Проверяет, заблокирован ли файл другим процессом
        /// </summary>
        private bool IsFileLocked(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            FileStream stream = null;
            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
        }

        /// <summary>
        /// Пытается закрыть процесс Word, который удерживает файл
        /// </summary>
        private bool TryCloseWordProcess(string filePath)
        {
            try
            {
                var fileName = Path.GetFileName(filePath);
                var wordProcesses = System.Diagnostics.Process.GetProcessesByName("WINWORD");

                foreach (var process in wordProcesses)
                {
                    try
                    {
                        // Пытаемся корректно закрыть процесс
                        process.CloseMainWindow();
                        process.WaitForExit(2000); // Ждем 2 секунды

                        if (!process.HasExited)
                        {
                            // Если не закрылся, принудительно завершаем
                            process.Kill();
                        }

                        _logService.LogInfo($"Закрыт процесс Word (PID: {process.Id})");
                    }
                    catch (Exception ex)
                    {
                        _logService.LogWarning($"Не удалось закрыть процесс Word: {ex.Message}");
                    }
                }

                return wordProcesses.Length > 0;
            }
            catch (Exception ex)
            {
                _logService.LogError($"Ошибка при попытке закрыть Word: {ex.Message}");
                return false;
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
