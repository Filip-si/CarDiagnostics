using CarDiagnostics.API.src.Modules.User.Api.Models;
using CarDiagnostics.API.src.Modules.User.Core.Dtos;
using CarDiagnostics.API.src.Modules.User.Core.Exceptions;
using CarDiagnostics.API.src.Modules.User.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDiagnostics.API.src.Modules.User.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;

    public UserRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddUser(UserRequest request)
    {
        var userExists = _dbContext.Users.AsNoTracking().Any(u => u.Email.ToLower().Trim() == request.Email.ToLower().Trim());
        if (userExists)
        {
            throw new EmailAlreadyUsedException();
        }

        var user = new UserEntity()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void DeleteUser(Guid id)
    {
        var user = _dbContext.Users
            .FirstOrDefault(u => u.Id == id) ?? throw new UserNotFoundException();

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }

    public UserDto GetUser(Guid id)
    {
        var user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == id);

        return user == null ? throw new UserNotFoundException() : new UserDto(user.Id, user.FirstName, user.LastName, user.Email);
    }

    public IEnumerable<UserDto> GetUsers()
    {
        var users = _dbContext.Users
            .AsNoTracking()
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email))
            .ToList();

        return users;
    }

    public void UpdateUser(UpdateUserRequest request)
    {
        var user = _dbContext.Users
            .FirstOrDefault(u => u.Id == request.Id) ?? throw new UserNotFoundException();

        if (user.Email.ToLower().Trim() != request.Email.ToLower().Trim())
        {
            var userExists = _dbContext.Users.AsNoTracking().Any(u => u.Email.ToLower().Trim() == request.Email.ToLower().Trim());
            if (userExists)
            {
                throw new EmailAlreadyUsedException();
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email.ToLower().Trim();

        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
}
