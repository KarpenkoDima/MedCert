using LiteDB;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace MedCert.Tests.Data.Repositories
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private Mock<ILogService> _mockLogService;
        private DatabaseOptions _dbOptions;
        private CustomerRepository _repository;
        private string _testDbPath;
        private LiteDatabase _db;
        [SetUp]
        public void SetUp()
        {
            _mockLogService = new Mock<ILogService>();

            // Создаем временную базу данных для тестов
            _testDbPath = Path.Combine(Path.GetTempPath(), $"test_db_{Guid.NewGuid()}.db");
            _db = new LiteDatabase(_testDbPath);
            _dbOptions = new DatabaseOptions
            {
                ConnectionString = $"Filename={_testDbPath}",
                EnableLogging = false,
                CommandTimeout = 30,
                EnableCaching = false,
                CacheTimeout = 300
            };

            _repository = new CustomerRepository(_db, _mockLogService.Object, _dbOptions);
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
        public void Add_ValidCustomer_Success()
        {
            // Arrange
            var customer = new Customer
            {
                FIO = "Тестовый Тест Тестович",
                BoD = new DateTime(1980, 5, 15),
                MedDate = DateTime.Now,
                Time = DateTime.Now,
                Sex = 1,
                Registration = "г. Киев, ул. Тестовая, 1",
                MedCheck = "Здоров",
                MedAnalisys = "Без патологий",
                R1 = 1,
                R2 = 2,
                MedDoctors = "Петров П.П."
            };

            // Act
            _repository.Add(customer);

            // Assert
            var allCustomers = _repository.GetAll();
            Assert.That(1, Is.EqualTo( allCustomers.Count));
            Assert.That("Тестовый Тест Тестович", Is.EqualTo( allCustomers[0].FIO));
        }

        [Test]
        public void GetAll_ReturnsAllCustomers()
        {
            // Arrange
            var customer1 = CreateTestCustomer("Алексеев А.А.");
            var customer2 = CreateTestCustomer("Борисов Б.Б.");
            var customer3 = CreateTestCustomer("Васильев В.В.");

            _repository.Add(customer1);
            _repository.Add(customer2);
            _repository.Add(customer3);

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.That(3, Is.EqualTo(result.Count));
            // Проверяем, что результаты отсортированы по ФИО
            Assert.That("Алексеев А.А.", Is.EqualTo(result[0].FIO));
            Assert.That("Борисов Б.Б.", Is.EqualTo(result[1].FIO));
            Assert.That("Васильев В.В.", Is.EqualTo(result[2].FIO));
        }

        [Test]
        public void Search_FindsCustomersByFIO()
        {
            // Arrange
            var customer1 = CreateTestCustomer("Иванов Иван Иванович");
            var customer2 = CreateTestCustomer("Иванова Мария Петровна");
            var customer3 = CreateTestCustomer("Петров Петр Петрович");

            _repository.Add(customer1);
            _repository.Add(customer2);
            _repository.Add(customer3);

            // Act
            var result = _repository.Search("Иван");

            // Assert
            Assert.That(2, Is.EqualTo(result.Count));
            Assert.That(result.Any(c => c.FIO == "Иванов Иван Иванович"));
            Assert.That(result.Any(c => c.FIO == "Иванова Мария Петровна"));
        }

        [Test]
        public void Delete_RemovesCustomer()
        {
            // Arrange
            var customer = CreateTestCustomer("Удаляемый У.У.");
            _repository.Add(customer);

            var allCustomers = _repository.GetAll();
            var customerId = allCustomers[0].Id;

            // Act
            _repository.Delete(customerId);

            // Assert
            var remainingCustomers = _repository.GetAll();
            Assert.That(0, Is.EqualTo(remainingCustomers.Count));
        }

        [Test]
        public void Update_ModifiesCustomer()
        {
            // Arrange
            var customer = CreateTestCustomer("Старое Имя");
            _repository.Add(customer);

            var allCustomers = _repository.GetAll();
            var customerToUpdate = allCustomers[0];
            customerToUpdate.FIO = "Новое Имя";

            // Act
            _repository.Update(customerToUpdate);

            // Assert
            var updatedCustomers = _repository.GetAll();
            Assert.That(1, Is.EqualTo(updatedCustomers.Count));
            Assert.That("Новое Имя", Is.EqualTo(updatedCustomers[0].FIO));
        }

        private Customer CreateTestCustomer(string fio)
        {
            return new Customer
            {
                FIO = fio,
                BoD = new DateTime(1980, 5, 15),
                MedDate = DateTime.Now,
                Time = DateTime.Now,
                Sex = 1,
                Registration = "Тестовый адрес",
                MedCheck = "Здоров",
                MedAnalisys = "Без патологий",
                R1 = 1,
                R2 = 2,
                MedDoctors = "Тестовый врач"
            };
        }
    }
}
