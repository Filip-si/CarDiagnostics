using System.ComponentModel.DataAnnotations;

namespace CarDiagnostics.API.src.Modules.User.Api.Models;

public class UpdateUserRequest : UserRequest
{
    [Required]
    public Guid Id { get; set; }
    public UpdateUserRequest(Guid id, string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
