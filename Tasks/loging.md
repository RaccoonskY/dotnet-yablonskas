# Логирование

Логирование (ведение журнала) - это запись логов, структурирование и перемещение их в отдельные файлы для
быстрого доступа к ним. Более продвинутый уровень записи хронологии позволяет классифицировать
логи по важности, в некоторых случаях даже удалять ненужные.

ASP.NET Core имеет встроенную поддержку логирования (ведения журнала). Логирование в ASP.NET Core основано на [использовании интерфейса
`ILogger`](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0) и его реализации, которую можно настроить для записи журнала в различные источники, такие как консоль, файл, базу данных и т.д.

### Serilog
[Serilog](https://serilog.net/) - это библиотека .NET для логирования, поддерживающая структурированные логи.

Основные преимущества Serilog включают:
1. Гибкость: Serilog позволяет настроить вывод логов в различные источники, такие как консоль, файл, база данных или любой другой пользовательский источник.
2. Структурированные логи: Serilog позволяет создавать логи с помощью структурированных данных в формате JSON, что делает их более удобными для анализа и обработки.
3. Расширяемость: Serilog можно легко расширять за счет использования плагинов и настройки пользовательских источников логов.
4. Поддержка .NET Standard: Serilog поддерживает .NET Standard, что позволяет использовать его на различных платформах и в различных приложениях, включая приложения ASP.NET, .NET Core и Xamarin.
5. Легкость использования: Serilog имеет простой API и легок в использовании, что делает его популярным выбором для разработчиков.

### Seq
[Seq](https://datalust.co/seq) - это высокопроизводительная платформа анализа данных, которая предназначена \
для обработки и анализа данных журналов (logs). Seq позволяет собирать, хранить и анализировать журналы 
в реальном времени.

![](https://files.readme.io/0a15c1b-Seq_in_Context.png)
Преимущества использования Seq для логирования:
1. Удобный интерфейс: Seq имеет легко настраиваемый веб-интерфейс, который позволяет просматривать и анализировать логи событий в режиме реального времени.
2. Структурированные логи: Seq поддерживает структурированные логи, что означает, что данные событий могут быть просто и точно фильтрованы, искать и сортировать. Это помогает обнаруживать проблемы быстрее и эффективнее.
3. Интеграция с сервисами Microsoft: Seq легко интегрируется с другими сервисами и инструментами от Microsoft, такими как Azure, Visual Studio, Team Foundation Server и другие.
4. Поддержка .NET: Seq имеет богатую функциональность для работы с приложениями, построенными на платформе .NET, включая логирование в коде и использование стандартных средств логирования, таких как Serilog, NLog и log4net.
5. Масштабируемость: Seq может быть легко масштабирован для обработки больших объемов логов и обеспечения высокой доступности.

## Практика
Настройте ведение логов с помощью [Serilog](https://serilog.net/) и [Seq](https://datalust.co/seq).

1. Установите пакеты Serilog для ASP.NET Core и Serilog.Sinks.Seq из NuGet:
```
Install-Package Serilog.AspNetCore
Install-Package Serilog.Sinks.Seq
```
2. Добавьте Seq в конфигурацию логирования Serilog в файле Program.cs:
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
```
3. Добавьте Serilog в конвейер обработки запросов в методе `Configure` класса `Startup.cs`:
```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSerilogRequestLogging();
        // ...
    }
}
```
После выполнения этих шагов ваше приложение будет записывать логи в Seq по адресу http://localhost:5341.
В файле ```docker-compose.yml``` уже насроен сервис Seq именно на этом порту.

## Теория

- [Ведение журнала в .NET Core и ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0)
- [Ведение лога и ILogger на Metanit](https://metanit.com/sharp/aspnet6/7.1.php)
- [Настройка Seq с помощью Serilog](https://docs.datalust.co/docs/using-serilog)
