namespace CarDiagnostics.API.src.Modules.User.Core.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base($"User not found.")
    {
    }
}
