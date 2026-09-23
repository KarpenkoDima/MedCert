using System;
using System.IO;
using LiteDB;
using Moq;
using NUnit.Framework;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace MedCert.Tests.Data.Repositories
{
    [TestFixture]
    public class TextTemplateRepositoryTests
    {
        private Mock<ILogService> _mockLogService;
        private DatabaseOptions _dbOptions;
        private TextTemplateRepository _repository;
        private string _testDbPath;
        private LiteDatabase _db;

        [SetUp]
        public void SetUp()
        {
            _mockLogService = new Mock<ILogService>();

            _testDbPath = Path.Combine(Path.GetTempPath(), $"test_db_{Guid.NewGuid()}.db");
            _db = new LiteDatabase(_testDbPath);
            _dbOptions = new DatabaseOptions()
            {
                ConnectionString = $"Filename={_testDbPath}",
                EnableCaching = false,
                CommandTimeout = 30,
                CacheTimeout = 300
            };

            _repository = new TextTemplateRepository(_db, "TextTemplate", _mockLogService.Object, _dbOptions);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testDbPath))
            {
                try
                {
                    File.Delete(_testDbPath);
                }
                catch (Exception ex)
                {
                    // игнорируем ошибки при очистке
                }
            }
        }

        [Test]
        public void Add_ValidTemplate_Success()
        {
            // Arrange
            var template = CreateTestTemplate("MedCheck", "Здоров");

            // Act
            _repository.Add(template);
            
            // Assert
            var all = _repository.GetAll();
            Assert.That(1, Is.EqualTo(all.Count));
            Assert.That("Здоров", Is.EqualTo((all[0].Text)));
        }
        
        [Test]
        public void GetAll_OrdersByCategory()
        {
            // Arrange
            _repository.Add(CreateTestTemplate("MedAnalisys", "Без патологий"));
            _repository.Add(CreateTestTemplate("MedCheck", "Здоров"));

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.That(2, Is.EqualTo(result.Count));
            Assert.That("MedAnalisys", Is.EqualTo(result[0].Category));
            Assert.That("MedCheck", Is.EqualTo(result[1].Category));
        }

        [Test]
        public void Update_ModifiesText()
        {
            // Arrange
            var template = CreateTestTemplate("MedCheck", "Старый текст");
            _repository.Add(template);

            var all = _repository.GetAll();
            var toUpdate = all[0];
            toUpdate.Text = "Новый текст";

            // Act
            _repository.Update(toUpdate);

            // Assert
            var updated = _repository.GetAll();
            Assert.That(1, Is.EqualTo(updated.Count));
            Assert.That("Новый текст", Is.EqualTo(updated[0].Text));
        }

        [Test]
        public void Delete_RemovesTemplate()
        {
            // Arrange
            var template = CreateTestTemplate("MedCheck", "Удаляемый шаблон");
            _repository.Add(template);

            var all = _repository.GetAll();
            var id = all[0].Id;

            // Act
            _repository.Delete(id);

            // Assert
            var remaining = _repository.GetAll();
            Assert.That(0, Is.EqualTo(remaining.Count));
        }

        private TextTemplate CreateTestTemplate(string category, string text)
        {
            return new TextTemplate()
            {
                Category = category,
                Text = text
            };
        }
    }
}