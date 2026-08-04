using System;
using System.Globalization;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public enum CertificateIssuanceStatus
    {
        PrintedAndSaved,
        RepeatPrintConfirmed,
        PrintNotConfirmed,
        DocumentGenerationFailed,
        StorageFailed
    }

    public sealed class CertificateIssuanceResult
    {
        public CertificateIssuanceResult(
            CertificateIssuanceStatus status,
            string error = null)
        {
            Status = status;
            Error = error;
        }

        public CertificateIssuanceStatus Status { get; }
        public string Error { get; }
    }

    public interface ICertificateIssuanceService
    {
        CertificateIssuanceResult Issue(Customer customer, bool isRepeatPrint);
    }

    public sealed class CertificateIssuanceService : ICertificateIssuanceService
    {
        private readonly IPrintService _printService;
        private readonly IPrintConfirmationService _printConfirmationService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogService _logService;

        public CertificateIssuanceService(
            IPrintService printService,
            IPrintConfirmationService printConfirmationService,
            ICustomerRepository customerRepository,
            ILogService logService)
        {
            _printService = printService ?? throw new ArgumentNullException(nameof(printService));
            _printConfirmationService = printConfirmationService ??
                throw new ArgumentNullException(nameof(printConfirmationService));
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public CertificateIssuanceResult Issue(Customer customer, bool isRepeatPrint)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var printData = CreatePrintData(customer);
            if (!_printService.PrintCertificate(printData, out var printError))
            {
                return new CertificateIssuanceResult(
                    CertificateIssuanceStatus.DocumentGenerationFailed,
                    printError ?? "Не удалось сформировать или открыть сертификат.");
            }

            if (!_printConfirmationService.ConfirmSuccessfulPrint())
            {
                _logService.LogWarning(
                    "Сертификат сформирован, но оператор не подтвердил успешную печать");
                return new CertificateIssuanceResult(
                    CertificateIssuanceStatus.PrintNotConfirmed);
            }

            if (isRepeatPrint)
            {
                _logService.LogInfo("Оператор подтвердил повторную печать сертификата");
                return new CertificateIssuanceResult(
                    CertificateIssuanceStatus.RepeatPrintConfirmed);
            }

            try
            {
                _customerRepository.Add(customer);
                return new CertificateIssuanceResult(
                    CertificateIssuanceStatus.PrintedAndSaved);
            }
            catch (Exception ex)
            {
                _logService.LogError(
                    "Сертификат распечатан, но запись не сохранена",
                    ex);
                return new CertificateIssuanceResult(
                    CertificateIssuanceStatus.StorageFailed,
                    "Сертификат распечатан, но запись о нём не удалось сохранить в базе данных.");
            }
        }

        private static PrintCertificateData CreatePrintData(Customer customer)
        {
            return new PrintCertificateData(
                customer.MedDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                customer.Time.ToString("HH:mm", CultureInfo.InvariantCulture),
                customer.FIO,
                customer.BoD.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                customer.Sex.ToString(CultureInfo.InvariantCulture),
                customer.Registration,
                customer.MedCheck,
                customer.R1.ToString(CultureInfo.InvariantCulture),
                customer.MedAnalisys,
                customer.R2.ToString(CultureInfo.InvariantCulture),
                customer.MedDoctors);
        }
    }
}
