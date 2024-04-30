using Core.User;
using Core.User.Aggregates;
using Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Triplex.Validations;

namespace BookApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// logs the user in from a body request and returns the user back if valid to indicate successful login
    /// </summary>
    /// <param name="userLoginData"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUser userLoginData)
    {
        Arguments.NotNull(userLoginData, nameof(userLoginData));

        User user = await _userService.LoginUser(userLoginData).ConfigureAwait(true);

        return Ok(user);
    }
    
    /// <summary>
    /// Gets all users from the database
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<User> users = await _userService.GetAllUsers().ConfigureAwait(true);

        return Ok(users);
    }
}