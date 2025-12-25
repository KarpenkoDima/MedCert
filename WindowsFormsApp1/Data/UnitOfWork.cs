using LiteDB;
using System;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Data
{
    /// <summary>
    /// Реализация паттерна Unit of Work для LiteDB
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LiteDatabase _database;
        private readonly ILogService _logService;
        private readonly DatabaseOptions _dbOptions;
        private bool _disposed;
        private bool _transactionStarted;

        private ICustomerRepository _customers;
        private IDoctorRepository _doctors;
        private ILogRepository _logs;

        public UnitOfWork(DatabaseOptions dbOptions, ILogService logService)
        {
            _dbOptions = dbOptions ?? throw new ArgumentNullException(nameof(dbOptions));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));

            // Создаем подключение к базе данных
            _database = new LiteDatabase(_dbOptions.ConnectionString);
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new CustomerRepository(_logService, _dbOptions);
                }
                return _customers;
            }
        }

        public IDoctorRepository Doctors
        {
            get
            {
                if (_doctors == null)
                {
                    _doctors = new DoctorRepository(_logService, _dbOptions);
                }
                return _doctors;
            }
        }

        public ILogRepository Logs
        {
            get
            {
                if (_logs == null)
                {
                    _logs = new LogRepository(_logService, _dbOptions);
                }
                return _logs;
            }
        }

        public void BeginTransaction()
        {
            if (_transactionStarted)
            {
                throw new InvalidOperationException("Транзакция уже запущена");
            }

            _database.BeginTrans();
            _transactionStarted = true;

            if (_dbOptions.EnableLogging)
            {
                _logService.LogInfo("Транзакция начата");
            }
        }

        public void Commit()
        {
            if (!_transactionStarted)
            {
                throw new InvalidOperationException("Транзакция не была начата");
            }

            try
            {
                _database.Commit();
                _transactionStarted = false;

                if (_dbOptions.EnableLogging)
                {
                    _logService.LogInfo("Транзакция зафиксирована");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError("Ошибка при фиксации транзакции", ex);
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            if (!_transactionStarted)
            {
                return;
            }

            try
            {
                _database.Rollback();
                _transactionStarted = false;

                if (_dbOptions.EnableLogging)
                {
                    _logService.LogInfo("Транзакция откатана");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError("Ошибка при откате транзакции", ex);
                throw;
            }
        }

        public int SaveChanges()
        {
            // LiteDB автоматически сохраняет изменения
            // Этот метод для совместимости с паттерном UoW
            if (_dbOptions.EnableLogging)
            {
                _logService.LogInfo("Изменения сохранены");
            }
            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Откатываем незафиксированную транзакцию
                    if (_transactionStarted)
                    {
                        try
                        {
                            Rollback();
                        }
                        catch (Exception ex)
                        {
                            _logService.LogError("Ошибка при откате транзакции в Dispose", ex);
                        }
                    }

                    // Освобождаем ресурсы
                    _database?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
