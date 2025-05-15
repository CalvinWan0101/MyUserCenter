using MyUserCenter.Service.Dto;

namespace MyUserCenter.Service;

public interface IMyUserService
{
    Task<MyUserDto> RegisterAsync(UserRegisterDto dto);
    Task<MyUserDto> LoginAsync(UserLoginDto dto);
    Task<MyUserDto?> GetByIdAsync(string id);
    Task<MyUserDto> UpdateAsync(UserUpdateDto dto);
}
