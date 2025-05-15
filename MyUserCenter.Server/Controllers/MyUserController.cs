using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyUserCenter.Domain;
using MyUserCenter.Service;
using MyUserCenter.Service.Dto;

[Route("[controller]")]
public class MyUserController : ControllerBase
{
    private readonly IMyUserService _userService;
    private readonly IPasswordHasher<MyUser> _passwordHasher;

    public MyUserController(
        IMyUserService userService,
        IPasswordHasher<MyUser> passwordHasher)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("register")]
    public async Task<JsonResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _userService.RegisterAsync(dto);
        return new JsonResult(result);
    }

    [HttpPost("login")]
    public async Task<JsonResult> Login([FromBody] UserLoginDto dto)
    {
        var result = await _userService.LoginAsync(dto);
        return new JsonResult(result);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<JsonResult> GetUser(string id)
    {
        var result = await _userService.GetByIdAsync(id);
        return new JsonResult(result);
    }

    [HttpPut("{id}")]
    public async Task<JsonResult> Update(string id, [FromBody] UserUpdateDto dto)
    {
        var result = await _userService.UpdateAsync(dto);
        return new JsonResult(result);
    }
}
