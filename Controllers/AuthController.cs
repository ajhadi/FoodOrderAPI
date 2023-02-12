using FoodOrderAPI.Models.DTOs;
using FoodOrderAPI.Services.TokenService;
using FoodOrderAPI.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace FoodOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly ITokenService tokenService;
    private readonly IUserService userService;

    public AuthController(IConfiguration configuration,
        ITokenService tokenService,
        IUserService userService
    )
    {
        this.configuration = configuration;
        this.tokenService = tokenService;
        this.userService = userService;
    }
    /// <summary>
    /// Login with username password
    /// </summary>
    /// <param name="request">Parameter</param>
    /// <returns>Token object</returns>
    [HttpPost("Login")]
    public async Task<ActionResult<TokenDTO>> LoginAsync(UserDTO request)
    {
        var getUser = await userService.GetUserByUsername(request.Username);
        if (!getUser.IsSuccess)
            throw new ApiException(getUser.Error);

        if (!tokenService.VerifyPasswordHash(request.Password, getUser.Result.PasswordHash, getUser.Result.PasswordSalt))
            throw new ApiException(AppError.WrongPassword);

        string token = tokenService.CreateToken(getUser.Result);
        return Ok(new TokenDTO{ Token = token });
    }
}