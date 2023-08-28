using System.Diagnostics.CodeAnalysis;

namespace Dotnet.Web.Exceptions;

public class UserFriendlyException : Exception
{
    public UserFriendlyException() { }
    
    public UserFriendlyException(string message)
        : base(message) { }
    
    public static void ThrowIfNull(object? argument, string text)
    {
        if (argument is null)
        {
            Throw(text);
        }
    }
    
    public static void ThrowIfFalse(bool argument, string text)
    {
        if (argument == false)
        {
            Throw(text);
        }
    }

    [DoesNotReturn]
    private static void Throw(string text) =>
        throw new UserFriendlyException(text);
}