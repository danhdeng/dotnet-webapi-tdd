using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    //private readonly ILogger<UsersController> _logger;

    //public UsersController(ILogger<UsersController> logger)
    //{
    //    _logger = logger;
    //}

    private readonly IUsersService _usersService;
    public UsersController(IUsersService usersService) {
        this._usersService = usersService;
    }
    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _usersService.GetAllUsers();
        if (users.Any())
        {
            return Ok(users);
        }
        return NotFound();
    }
}
