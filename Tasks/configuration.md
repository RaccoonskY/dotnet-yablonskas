# Конфигурация

Конфигурация среды выполнения позволяет настроить свойства приложения, зависящие от текущей среды выполнения. 
Например, можно настроить имя хоста и порт, на котором приложение будет слушать запросы во время разработки, а 
в продакшен-среде использовать другой хост и порт.

ASP.NET Core поддерживает несколько способов конфигурации среды выполнения, включая:

- Файлы конфигурации: можно использовать отдельные файлы конфигурации для каждой среды выполнения. Например, `appsettings.Development.json` для разработки, `appsettings.Production.json` для продакшена и т.д. В каждом файле можно указать значения параметров для конкретной среды выполнения.
- Переменные среды: можно использовать переменные среды для настройки свойств приложения в зависимости от текущей среды выполнения. Например, можно использовать переменные среды `ASPNETCORE_ENVIRONMENT` и `ASPNETCORE_URLS` для указания текущей среды выполнения и URL-адреса, на котором приложение будет слушать запросы.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        // Настройки для среды выполнения "Development"
    }
    else if (env.IsProduction())
    {
        // Настройки для среды выполнения "Production"
    }

    // Настройки, общие для всех сред выполнения
}

public void ConfigureServices(IServiceCollection services)
{
    if (Environment.IsDevelopment())
    {
        // Использовать более легковесные или упрощенные реализации сервисов для среды "Development"
        services.AddSingleton<IMyService, MyServiceDev>();
    }
    else
    {
        // Использовать более мощные и производительные реализации сервисов для других сред
        services.AddSingleton<IMyService, MyService>();
    }
}
```

## Практика
- Сконфигурируйте приложение для работы в двух режимах: `dev` - локально установленный [Postgres](https://www.npgsql.org/efcore/), `test` - in-memory реализация.
- Используйте [миграции](https://learn.microsoft.com/ru-ru/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli), чтобы создать схему БД при использовании Postgres. 
- Используйте конструкцию [`builder.UseEnvironment("Test");`](https://learn.microsoft.com/ru-ru/aspnet/core/test/integration-tests?view=aspnetcore-7.0), чтобы установить необходимое окружение в режиме тестов.

## Теория
- [Конфигурация в .NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0)
- [Как работает конфигурация в .NET Core](https://habr.com/ru/post/453416/)
- [.NET Configuration In Depth | .NET Conf 2022](https://www.youtube.com/watch?v=1aNMO2cBmv0)
- [Миграции EF Core](https://learn.microsoft.com/ru-ru/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
