namespace CarDiagnostics.API.src.Modules.User.Core.Exceptions;

public class EmailAlreadyUsedException : Exception
{
    public EmailAlreadyUsedException() : base($"User with this email already exists.")
    {
    }
}
