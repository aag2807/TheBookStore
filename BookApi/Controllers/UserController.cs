using Boundaries.Persistance.Repositories.User;
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
    /// 
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
}