﻿using CarDiagnostics.API.src.Modules.User.Api.Models;
using CarDiagnostics.API.src.Modules.User.Core.Dtos;

namespace CarDiagnostics.API.src.Modules.User.Core.Services;

public interface IUserService
{
    public void AddUser(UserRequest request);
    public UserDto GetUser(Guid id);
    public IEnumerable<UserDto> GetUsers();
    public void UpdateUser(UpdateUserRequest request);
    public void DeleteUser(Guid id);
}