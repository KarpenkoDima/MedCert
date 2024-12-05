# MedCert - Приложение для печати медицинских сертификатов

## Описание
MedCert - это однопользовательское Windows-приложение, разработанное для автоматизации процесса создания и печати медицинских сертификатов. Приложение генерирует сертификаты по заданному шаблону в формате DOCX и отправляет их на печать, с последующим сохранением информации в базе данных.

## Основные функции
- Генерация медицинских сертификатов по шаблону
- Экспорт сертификатов в формат DOCX
- Прямая отправка документов на печать
- Сохранение информации о выданных сертификатах в базе данных
- Просмотр истории выданных сертификатов

## Технологии
- .NET Framework 4.8
- Windows Forms
- LiteDB (встраиваемая NoSQL база данных)
- Microsoft.Office.Interop.Word
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.DependencyInjection

## Системные требования
- Windows 7 и выше
- .NET Framework 4.8
- Microsoft Word (для работы с DOCX файлами)
- Принтер (физический или виртуальный)

## Установка и запуск
1. Скачайте последнюю версию приложения
2. Распакуйте архив в папку "MedCert"
3. Убедитесь, что присутствует следующая структура папок:
   ```
   MedCert/
   ├── Data/
   │   └── DB/
   │       └── database.db
   └── Resources/
       └── TemplatesDocx/
           ├── template_form100o_2.docx
           └── output.doc
   ```
4. Запустите файл MedCert.exe
5. При первом запуске приложение создаст необходимую структуру базы данных

## Конфигурация
Основные настройки находятся в файле App.config:
```xml
<configuration>
  <connectionStrings>
    <add name="LiteDB" connectionString="Filename=.\Data\DB\database.db" />
  </connectionStrings>
  <appSettings>
    <add key="EnableLogging" value="true" />
    <add key="CommandTimeout" value="30" />
    <add key="EnableCaching" value="true" />
    <add key="CacheTimeout" value="300" />
    <add key="templateDocx" value="\Resources\TemplatesDocx\template_form100o_2.docx" />
    <add key="outputDoc" value="\Resources\TemplatesDocx\output.doc" />
  </appSettings>
</configuration>
```

## Разработка
Для работы с исходным кодом требуется:
1. Visual Studio 2022
2. .NET Framework 4.8 SDK
3. Установленные NuGet пакеты (см. раздел "Используемые пакеты NuGet")

## Структура решения
```
MedCert.sln
└── WindowsFormsApp1/
    └── MedCert.csproj
```

## Версия Visual Studio
- Visual Studio 2022 (Version 17.5.33502.453)
- Minimum Version = 10.0.40219.1

## Автор
Карпенко Д/КНП ОКПЛ
