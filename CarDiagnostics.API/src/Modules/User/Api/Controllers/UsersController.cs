using CarDiagnostics.API.src.Modules.User.Api.Models;
using CarDiagnostics.API.src.Modules.User.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDiagnostics.API.src.Modules.User.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult AddUser(UserRequest request)
    {
        _userService.AddUser(request);
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateUser(UpdateUserRequest request)
    {
        _userService.UpdateUser(request);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _userService.GetUser(id);
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        _userService.DeleteUser(id);
        return Ok();
    }
}

