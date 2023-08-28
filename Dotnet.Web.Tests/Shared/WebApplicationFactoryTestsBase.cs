using System.Net.Http.Headers;
using Dotnet.Web.Controllers;
using Dotnet.Web.Data;
using Dotnet.Web.Exceptions;
using Dotnet.Web.Refit;
using Refit;

namespace Dotnet.Web.Tests.Shared;

public class WebApplicationFactoryTestsBase: IClassFixture<DotnetWebApplicationFactory> 
{
    protected readonly DotnetWebApplicationFactory Factory;
   
    public WebApplicationFactoryTestsBase(DotnetWebApplicationFactory factory)
    {
        Factory = factory;
    }

    private HttpClient? _httpClient;

    public HttpClient HttpClient => _httpClient ??= Factory.CreateClient();
}

public class WebApplicationFactoryTestsBase<T> : WebApplicationFactoryTestsBase
{
    public WebApplicationFactoryTestsBase(DotnetWebApplicationFactory factory) : base(factory) { }

    protected T CreateControllerClient() => RestService.For<T>(HttpClient);

    protected TController CreateControllerClient<TController>() => RestService.For<TController>(HttpClient);

    protected async Task LoginAsAdminAsync()
    {
        var users = RestService.For<IUsersController>(HttpClient);
        var response = await users.Login(DbSeeder.Login);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
    }
    
    protected async Task LoginAsUserAsync()
    {
        var users = RestService.For<IUsersController>(HttpClient);
        var response = await users.Login(DbSeeder.LoginByUser);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
    }

    protected T Rest() => RestService.For<T>(HttpClient);

    
    protected bool IsMethodHave401UnauthorizedResponse(Func<Task> testCode)
    {
        try
        {
            var x = testCode.Invoke();
            x.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (e.Message == "One or more errors occurred. (Response status code does not indicate success: 401 (Unauthorized).)")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    protected bool IsMethodHave403UnauthorizedResponse(Func<Task> testCode)
    {
        try
        {
            var x = testCode.Invoke();
            x.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (e.Message == "One or more errors occurred. (Response status code does not indicate success: 403 (Forbidden).)")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    protected bool IsMethodHaveUserFriendlyException422dResponse(Func<Task> testCode)
    {
        try
        {
            var x = testCode.Invoke();
            x.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (e.Message == "One or more errors occurred. (Response status code does not indicate success: 422 (Unprocessable Entity).)" ||
                e.Message == "One or more errors occurred. (Response status code does not indicate success: 422 (Unprocessable Entity).)")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
