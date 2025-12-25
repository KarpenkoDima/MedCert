using Moq;
using NUnit.Framework;
using System;
using System.IO;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace MedCert.Tests.Data
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private Mock<ILogService> _mockLogService;
        private DatabaseOptions _dbOptions;
        private string _testDbPath;

        [SetUp]
        public void SetUp()
        {
            _mockLogService = new Mock<ILogService>();

            // Создаем временную базу данных для тестов
            _testDbPath = Path.Combine(Path.GetTempPath(), $"test_uow_{Guid.NewGuid()}.db");

            _dbOptions = new DatabaseOptions
            {
                ConnectionString = $"Filename={_testDbPath}",
                EnableLogging = true,
                CommandTimeout = 30,
                EnableCaching = false,
                CacheTimeout = 300
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Удаляем тестовую базу данных
            if (File.Exists(_testDbPath))
            {
                try
                {
                    File.Delete(_testDbPath);
                }
                catch
                {
                    // Игнорируем ошибки при очистке
                }
            }
        }

        [Test]
        public void UnitOfWork_Customers_ReturnsRepository()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                var customers = uow.Customers;

                // Assert
                Assert.IsNotNull(customers);
                Assert.IsInstanceOf<WindowsFormsApp1.Data.Repositories.ICustomerRepository>(customers);
            }
        }

        [Test]
        public void UnitOfWork_Doctors_ReturnsRepository()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                var doctors = uow.Doctors;

                // Assert
                Assert.IsNotNull(doctors);
                Assert.IsInstanceOf<WindowsFormsApp1.Data.Repositories.IDoctorRepository>(doctors);
            }
        }

        [Test]
        public void UnitOfWork_Logs_ReturnsRepository()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                var logs = uow.Logs;

                // Assert
                Assert.IsNotNull(logs);
                Assert.IsInstanceOf<WindowsFormsApp1.Data.Repositories.ILogRepository>(logs);
            }
        }

        [Test]
        public void BeginTransaction_StartsTransaction()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                uow.BeginTransaction();

                // Assert
                _mockLogService.Verify(x => x.LogInfo("Транзакция начата"), Times.Once);
            }
        }

        [Test]
        public void BeginTransaction_CalledTwice_ThrowsException()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                uow.BeginTransaction();

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => uow.BeginTransaction());
            }
        }

        [Test]
        public void Commit_WithoutBeginTransaction_ThrowsException()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => uow.Commit());
            }
        }

        [Test]
        public void Commit_AfterBeginTransaction_Success()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                uow.BeginTransaction();

                // Act
                uow.Commit();

                // Assert
                _mockLogService.Verify(x => x.LogInfo("Транзакция зафиксирована"), Times.Once);
            }
        }

        [Test]
        public void Rollback_WithoutBeginTransaction_DoesNothing()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act - не должно быть исключения
                uow.Rollback();

                // Assert
                _mockLogService.Verify(x => x.LogInfo("Транзакция откатана"), Times.Never);
            }
        }

        [Test]
        public void Rollback_AfterBeginTransaction_Success()
        {
            // Arrange
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                uow.BeginTransaction();

                // Act
                uow.Rollback();

                // Assert
                _mockLogService.Verify(x => x.LogInfo("Транзакция откатана"), Times.Once);
            }
        }

        [Test]
        public void Transaction_AddAndCommit_SavesData()
        {
            // Arrange
            var customer = new Customer
            {
                FIO = "Транзакционный Тест",
                BoD = new DateTime(1990, 1, 1),
                MedDate = DateTime.Now,
                Time = DateTime.Now,
                Sex = 1,
                Registration = "Тестовый адрес",
                MedCheck = "Здоров",
                MedAnalisys = "Без патологий",
                R1 = 1,
                R2 = 2,
                MedDoctors = "Врач"
            };

            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                uow.Commit();
            }

            // Assert - проверяем в новой сессии
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.AreEqual(1, customers.Count);
                Assert.AreEqual("Транзакционный Тест", customers[0].FIO);
            }
        }

        [Test]
        public void Transaction_AddAndRollback_DoesNotSaveData()
        {
            // Arrange
            var customer = new Customer
            {
                FIO = "Откатываемый Тест",
                BoD = new DateTime(1990, 1, 1),
                MedDate = DateTime.Now,
                Time = DateTime.Now,
                Sex = 1,
                Registration = "Тестовый адрес",
                MedCheck = "Здоров",
                MedAnalisys = "Без патологий",
                R1 = 1,
                R2 = 2,
                MedDoctors = "Врач"
            };

            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                // Act
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                uow.Rollback();
            }

            // Assert - проверяем в новой сессии
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.AreEqual(0, customers.Count);
            }
        }

        [Test]
        public void Dispose_WithPendingTransaction_RollsBack()
        {
            // Arrange
            var customer = new Customer
            {
                FIO = "Автоматический откат",
                BoD = new DateTime(1990, 1, 1),
                MedDate = DateTime.Now,
                Time = DateTime.Now,
                Sex = 1,
                Registration = "Тестовый адрес",
                MedCheck = "Здоров",
                MedAnalisys = "Без патологий",
                R1 = 1,
                R2 = 2,
                MedDoctors = "Врач"
            };

            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                // Не вызываем Commit - транзакция должна откатиться автоматически
            }

            // Assert - проверяем в новой сессии
            using (var uow = new UnitOfWork(_dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.AreEqual(0, customers.Count);
            }
        }
    }
}
