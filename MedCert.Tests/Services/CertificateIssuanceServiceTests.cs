using Moq;
using NUnit.Framework;
using System;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace MedCert.Tests.Services
{
    [TestFixture]
    public class CertificateIssuanceServiceTests
    {
        private Mock<IPrintService> _printService;
        private Mock<IPrintConfirmationService> _printConfirmationService;
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<ILogService> _logService;
        private CertificateIssuanceService _service;

        [SetUp]
        public void SetUp()
        {
            _printService = new Mock<IPrintService>();
            _printConfirmationService = new Mock<IPrintConfirmationService>();
            _customerRepository = new Mock<ICustomerRepository>();
            _logService = new Mock<ILogService>();

            _service = new CertificateIssuanceService(
                _printService.Object,
                _printConfirmationService.Object,
                _customerRepository.Object,
                _logService.Object);
        }

        [Test]
        public void Issue_WhenDocumentGenerationFails_DoesNotConfirmOrSave()
        {
            ConfigurePrint(false, "Ошибка Word");

            var result = _service.Issue(CreateCustomer(), false);

            Assert.That(
                result.Status,
                Is.EqualTo(CertificateIssuanceStatus.DocumentGenerationFailed));
            Assert.That(result.Error, Is.EqualTo("Ошибка Word"));
            _printConfirmationService.Verify(
                x => x.ConfirmSuccessfulPrint(),
                Times.Never);
            _customerRepository.Verify(
                x => x.Add(It.IsAny<Customer>()),
                Times.Never);
        }

        [Test]
        public void Issue_WhenPrintIsNotConfirmed_DoesNotSave()
        {
            ConfigurePrint(true);
            _printConfirmationService
                .Setup(x => x.ConfirmSuccessfulPrint())
                .Returns(false);

            var result = _service.Issue(CreateCustomer(), false);

            Assert.That(
                result.Status,
                Is.EqualTo(CertificateIssuanceStatus.PrintNotConfirmed));
            _customerRepository.Verify(
                x => x.Add(It.IsAny<Customer>()),
                Times.Never);
        }

        [Test]
        public void Issue_NewCertificateConfirmed_SavesCustomer()
        {
            var customer = CreateCustomer();
            string printError = null;
            _printService
                .Setup(x => x.PrintCertificate(
                    It.Is<PrintCertificateData>(data =>
                        data.CheckDate == "04.08.2026" &&
                        data.CheckTime == "09:30" &&
                        data.BirthDate == "15.05.1980"),
                    out printError))
                .Returns(true);
            _printConfirmationService
                .Setup(x => x.ConfirmSuccessfulPrint())
                .Returns(true);

            var result = _service.Issue(customer, false);

            Assert.That(
                result.Status,
                Is.EqualTo(CertificateIssuanceStatus.PrintedAndSaved));
            _customerRepository.Verify(x => x.Add(customer), Times.Once);
        }

        [Test]
        public void Issue_RepeatPrintConfirmed_DoesNotCreateDuplicate()
        {
            ConfigurePrint(true);
            _printConfirmationService
                .Setup(x => x.ConfirmSuccessfulPrint())
                .Returns(true);

            var result = _service.Issue(CreateCustomer(), true);

            Assert.That(
                result.Status,
                Is.EqualTo(CertificateIssuanceStatus.RepeatPrintConfirmed));
            _customerRepository.Verify(
                x => x.Add(It.IsAny<Customer>()),
                Times.Never);
        }

        [Test]
        public void Issue_WhenStorageFails_ReturnsErrorAndLogsException()
        {
            var customer = CreateCustomer();
            var exception = new InvalidOperationException("Database unavailable");
            ConfigurePrint(true);
            _printConfirmationService
                .Setup(x => x.ConfirmSuccessfulPrint())
                .Returns(true);
            _customerRepository
                .Setup(x => x.Add(customer))
                .Throws(exception);

            var result = _service.Issue(customer, false);

            Assert.That(
                result.Status,
                Is.EqualTo(CertificateIssuanceStatus.StorageFailed));
            Assert.That(result.Error, Is.Not.Empty);
            _logService.Verify(
                x => x.LogError(
                    "Сертификат распечатан, но запись не сохранена",
                    exception),
                Times.Once);
        }

        private void ConfigurePrint(bool result, string error = null)
        {
            var printError = error;
            _printService
                .Setup(x => x.PrintCertificate(
                    It.IsAny<PrintCertificateData>(),
                    out printError))
                .Returns(result);
        }

        private static Customer CreateCustomer()
        {
            return new Customer
            {
                FIO = "Тестовый Пациент",
                BoD = new DateTime(1980, 5, 15),
                MedDate = new DateTime(2026, 8, 4),
                Time = new DateTime(2026, 8, 4, 9, 30, 0),
                Sex = 1,
                Registration = "Тестовый адрес",
                MedCheck = "Работа",
                MedAnalisys = "Обследование",
                R1 = 2,
                R2 = 3,
                MedDoctors = "Тестовый Врач"
            };
        }
    }
}
