# MedCert.Tests - Unit тесты

## Описание

Этот проект содержит unit-тесты для приложения MedCert.

## Технологии

- **NUnit 3.13.3** - фреймворк для тестирования
- **Moq 4.18.4** - библиотека для создания mock-объектов
- **.NET Framework 4.8**

## Структура тестов

```
MedCert.Tests/
├── Data/
│   ├── Repositories/
│   │   └── CustomerRepositoryTests.cs   # Тесты репозитория клиентов
│   └── UnitOfWorkTests.cs               # Тесты паттерна Unit of Work
└── Services/
    └── PrintServiceTests.cs             # Тесты сервиса печати
```

## Запуск тестов

### Visual Studio

1. Откройте решение `Medcert.sln` в Visual Studio
2. Выполните сборку решения (Build > Build Solution)
3. Откройте Test Explorer (Test > Test Explorer)
4. Нажмите "Run All" для запуска всех тестов

### Командная строка

Для запуска тестов из командной строки используйте NUnit Console Runner:

```bash
# Установите NuGet пакеты
nuget restore Medcert.sln

# Соберите проект
msbuild Medcert.sln /p:Configuration=Debug

# Запустите тесты с помощью NUnit Console
packages\NUnit.ConsoleRunner.3.16.3\tools\nunit3-console.exe MedCert.Tests\bin\Debug\MedCert.Tests.dll
```

## Покрытие тестами

### PrintServiceTests

- ✅ Валидация данных сертификата
- ✅ Нормализация дат и времени
- ✅ Логирование ошибок, предупреждений и информации

### CustomerRepositoryTests

- ✅ Добавление клиента в БД
- ✅ Получение всех клиентов
- ✅ Поиск клиентов по ФИО
- ✅ Удаление клиента
- ✅ Обновление данных клиента

### UnitOfWorkTests

- ✅ Получение репозиториев через UoW
- ✅ Начало транзакции
- ✅ Фиксация транзакции
- ✅ Откат транзакции
- ✅ Автоматический откат при dispose
- ✅ Сохранение данных с транзакцией
- ✅ Откат данных при rollback

## Важные замечания

1. **Временные файлы**: Тесты создают временные базы данных и файлы, которые автоматически удаляются после выполнения
2. **Изоляция**: Каждый тест использует отдельную БД для обеспечения изоляции
3. **Mocking**: Зависимости (LogService) мокируются с помощью Moq

## Добавление новых тестов

Для добавления нового теста:

1. Создайте новый класс в соответствующей папке
2. Добавьте атрибут `[TestFixture]` к классу
3. Создайте методы тестирования с атрибутом `[Test]`
4. Используйте `[SetUp]` для инициализации перед каждым тестом
5. Используйте `[TearDown]` для очистки после каждого теста

### Пример

```csharp
[TestFixture]
public class MyServiceTests
{
    private Mock<IDependency> _mockDependency;
    private MyService _service;

    [SetUp]
    public void SetUp()
    {
        _mockDependency = new Mock<IDependency>();
        _service = new MyService(_mockDependency.Object);
    }

    [Test]
    public void Method_Scenario_ExpectedResult()
    {
        // Arrange
        var input = "test";

        // Act
        var result = _service.Method(input);

        // Assert
        Assert.AreEqual("expected", result);
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup
    }
}
```

## Continuous Integration

Тесты могут быть интегрированы в CI/CD pipeline для автоматического запуска при каждом коммите.
