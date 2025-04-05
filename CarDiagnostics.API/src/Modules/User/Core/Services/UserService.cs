using CarDiagnostics.API.src.Modules.User.Api.Models;
using CarDiagnostics.API.src.Modules.User.Core.Dtos;
using CarDiagnostics.API.src.Modules.User.Data.Repositories;

namespace CarDiagnostics.API.src.Modules.User.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(UserRequest request)
    {
        _userRepository.AddUser(request);
    }

    public UserDto GetUser(Guid id)
    {
        return _userRepository.GetUser(id);
    }

    public IEnumerable<UserDto> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public void UpdateUser(UpdateUserRequest request)
    {
        _userRepository.UpdateUser(request);
    }

    public void DeleteUser(Guid id)
    {
        _userRepository.DeleteUser(id);
    }
}
