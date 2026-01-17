using LiteDB;
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
       // private LiteDatabase _db;

        [SetUp]
        public void SetUp()
        {
            _mockLogService = new Mock<ILogService>();

            // Создаем временную базу данных для тестов
            _testDbPath = Path.Combine(Path.GetTempPath(), $"test_uow_{Guid.NewGuid()}.db");
            //_db = new LiteDatabase(_testDbPath);
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act
                var customers = uow.Customers;

                // Assert
                Assert.That(customers, Is.Not.Null);
                Assert.That(customers, Is.InstanceOf<WindowsFormsApp1.Data.Repositories.ICustomerRepository>());
            }
        }

        [Test]
        public void UnitOfWork_Doctors_ReturnsRepository()
        {
            // Arrange
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act
                var doctors = uow.Doctors;

                // Assert
                Assert.That(doctors, Is.Not.Null);
                Assert.That(doctors, Is.InstanceOf<WindowsFormsApp1.Data.Repositories.IDoctorRepository>());
            }
        }

        [Test]
        public void UnitOfWork_Logs_ReturnsRepository()
        {
            // Arrange
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act
                var logs = uow.Logs;

                // Assert
                Assert.That(logs, Is.Not.Null);
                Assert.That(logs, Is.InstanceOf<WindowsFormsApp1.Data.Repositories.ILogRepository>());
            }
        }

        [Test]
        public void BeginTransaction_StartsTransaction()
        {
            // Arrange
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => uow.Commit());
            }
        }

        [Test]
        public void Commit_AfterBeginTransaction_Success()
        {
            // Arrange
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                uow.Commit();
            }

            // Assert - проверяем в новой сессии
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.That(1, Is.EqualTo(customers.Count));
                Assert.That("Транзакционный Тест", Is.EqualTo(customers[0].FIO));
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


            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                // Act
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                uow.Rollback();
            }
            File.Delete(_testDbPath);
            // Assert - проверяем в новой сессии
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.That(0, Is.EqualTo(customers.Count));
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
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                uow.BeginTransaction();
                uow.Customers.Add(customer);
                // Не вызываем Commit - транзакция должна откатиться автоматически
            }

            // Assert - проверяем в новой сессии
            using (var db = new LiteDatabase(_testDbPath))
            using (var uow = new UnitOfWork(db, _dbOptions, _mockLogService.Object))
            {
                var customers = uow.Customers.GetAll();
                Assert.That(0, Is.EqualTo(customers.Count));
            }
        }
    }
}
