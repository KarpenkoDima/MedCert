using Moq;
using NUnit.Framework;
using System;
using System.IO;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace MedCert.Tests.Services
{
    [TestFixture]
    public class PrintServiceTests
    {
        private Mock<ILogService> _mockLogService;
        private string _testTemplateDir;
        private string _testTemplatePath;
        private string _testOutputPath;

        [SetUp]
        public void SetUp()
        {
            _mockLogService = new Mock<ILogService>();

            // Создаем временные директории для тестов
            _testTemplateDir = Path.Combine(Path.GetTempPath(), "MedCertTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testTemplateDir);

            _testTemplatePath = Path.Combine(_testTemplateDir, "template.docx");
            _testOutputPath = Path.Combine(_testTemplateDir, "output.doc");
        }

        [TearDown]
        public void TearDown()
        {
            // Очистка временных файлов
            if (Directory.Exists(_testTemplateDir))
            {
                try
                {
                    Directory.Delete(_testTemplateDir, true);
                }
                catch
                {
                    // Игнорируем ошибки при очистке
                }
            }
        }

        [Test]
        public void PrintCertificate_WithValidData_ReturnsTrue()
        {
            // Arrange
            var data = new PrintCertificateData(
                "01.01.2025",
                "10:00",
                "Иванов Иван Иванович",
                "15.05.1980",
                "Мужской",
                "г. Киев, ул. Тестовая, 1",
                "Здоров",
                "Наявні",
                "Без патологий",
                "Відсутні",
                "Петров П.П."
            );

            // Assert - проверяем, что данные корректно нормализованы
            Assert.AreEqual("01.01.2025", data.CheckDate);
            Assert.AreEqual("10:00", data.CheckTime);
            Assert.AreEqual("Иванов Иван Иванович", data.FullName);
        }

        [Test]
        public void PrintCertificateData_WithInvalidDate_NormalizesToSpaces()
        {
            // Arrange & Act
            var data = new PrintCertificateData(
                "01.01", // Неполная дата
                "10:00",
                "Тест",
                "15.05.1980",
                "М",
                "Адрес",
                "Результат",
                "1",
                "Обследование",
                "2",
                "Врач"
            );

            // Assert - должны быть пробелы вместо некорректной даты
            Assert.AreEqual("          ", data.CheckDate);
        }

        [Test]
        public void PrintCertificateData_WithInvalidTime_NormalizesToSpaces()
        {
            // Arrange & Act
            var data = new PrintCertificateData(
                "01.01.2025",
                "10", // Неполное время
                "Тест",
                "15.05.1980",
                "М",
                "Адрес",
                "Результат",
                "1",
                "Обследование",
                "2",
                "Врач"
            );

            // Assert - должны быть пробелы вместо некорректного времени
            Assert.AreEqual("     ", data.CheckTime);
        }

        [Test]
        public void LogService_LogError_InvokesCorrectly()
        {
            // Arrange
            var errorMessage = "Test error message";
            var exception = new Exception("Test exception");

            // Act
            _mockLogService.Object.LogError(errorMessage, exception);

            // Assert
            _mockLogService.Verify(x => x.LogError(errorMessage, exception), Times.Once);
        }

        [Test]
        public void LogService_LogInfo_InvokesCorrectly()
        {
            // Arrange
            var infoMessage = "Test info message";

            // Act
            _mockLogService.Object.LogInfo(infoMessage);

            // Assert
            _mockLogService.Verify(x => x.LogInfo(infoMessage), Times.Once);
        }

        [Test]
        public void LogService_LogWarning_InvokesCorrectly()
        {
            // Arrange
            var warningMessage = "Test warning message";

            // Act
            _mockLogService.Object.LogWarning(warningMessage);

            // Assert
            _mockLogService.Verify(x => x.LogWarning(warningMessage), Times.Once);
        }
    }
}
