using CloudCostumers.API.Controllers.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCostumers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService _userService)
    {
        this._userService = _userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        if(users.Any())
        {
            return Ok(users);
        }
        return NotFound();

        return UnprocessableEntity();
        
    }
}
