using System.ComponentModel.DataAnnotations;

namespace CarDiagnostics.API.src.Modules.User.Api.Models;

public class UserRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid e-mail address")]
    public string Email { get; set; }

    public UserRequest(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
