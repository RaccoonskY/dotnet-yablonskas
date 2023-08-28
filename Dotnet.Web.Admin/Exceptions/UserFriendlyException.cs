using System.Diagnostics.CodeAnalysis;

namespace Dotnet.Web.Admin.Exceptions;

public class UserFriendlyException : Exception
{
    public UserFriendlyException() { }
    
    public UserFriendlyException(string message)
        : base(message) { }
    
    public static void ThrowIfNull([NotNull] object? argument, string text)
    {
        if (argument is null)
        {
            Throw(text);
        }
    }

    [DoesNotReturn]
    private static void Throw(string text) =>
        throw new UserFriendlyException(text);
}