using System;
using WindowsFormsApp1.Data.Repositories;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Интерфейс Unit of Work для управления транзакциями и репозиториями
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий клиентов
        /// </summary>
        ICustomerRepository Customers { get; }

        /// <summary>
        /// Репозиторий врачей
        /// </summary>
        IDoctorRepository Doctors { get; }

        /// <summary>
        /// Репозиторий логов
        /// </summary>
        ILogRepository Logs { get; }

        /// <summary>
        /// Начинает новую транзакцию
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Фиксирует изменения транзакции
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатывает изменения транзакции
        /// </summary>
        void Rollback();

        /// <summary>
        /// Сохраняет все изменения в базе данных
        /// </summary>
        /// <returns>Количество затронутых записей</returns>
        int SaveChanges();
    }
}
