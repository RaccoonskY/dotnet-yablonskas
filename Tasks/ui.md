# UI

Не смотря на то что покупателями нашего магазина выступают роботы, администрируют магазин всё ещё люди.
Для них необходимо разработать пользовательский интерфейс. Для создания пользовательского интерфейса мы 
воспользуемся Razor Pages.

[Razor Pages](https://learn.microsoft.com/ru-ru/aspnet/core/razor-pages/?view=aspnetcore-7.0&tabs=visual-studio) - это фреймворк для создания веб-приложений на платформе ASP.NET Core. 
Он предоставляет простой способ создания веб-страниц, используя шаблонизацию Razor

Одним из ключевых отличий Razor Pages от ASP.NET Core MVC является то, что в Razor Pages отсутствует 
явный раздел между контроллером и представлением. В Razor Pages логика обработки запросов и отображения 
данных размещается внутри одной страницы (Page).


## Практика
Создайте область администрирования с помощью Razor Pages. Область администрирования должна 
быть доступна только для пользователей с ролью &laquo;администратор&raquo;. В области администрирования должна быть возможность
создавать, редактировать и удалять товары.

## Теория
- [Введение в Razor Pages в ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/razor-pages/?view=aspnetcore-7.0&tabs=visual-studio).
- [Формирование шаблонов Register, Login, LogOut и RegisterConfirmation](https://learn.microsoft.com/ru-ru/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio#scaffold-register-login-logout-and-registerconfirmation).