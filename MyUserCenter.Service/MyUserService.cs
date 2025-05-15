using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyUserCenter.Domain;
using MyUserCenter.EFCore;
using MyUserCenter.Service.Dto;

namespace MyUserCenter.Service;

public class MyUserService : IMyUserService
{
    private readonly MyUserCenterDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<MyUser> _passwordHasher;

    public MyUserService(
        MyUserCenterDbContext context,
        IMapper mapper,
        IPasswordHasher<MyUser> passwordHasher)
    {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<MyUserDto> RegisterAsync(UserRegisterDto dto)
    {
        if (await _context.MyUsers.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("User already exists.");

        var newUser = new MyUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
        };
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

        _context.MyUsers.Add(newUser);
        await _context.SaveChangesAsync();

        return _mapper.Map<MyUserDto>(newUser);
    }

    public async Task<MyUserDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _context.MyUsers.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid credentials.");

        return _mapper.Map<MyUserDto>(user);
    }

    public async Task<MyUserDto?> GetByIdAsync(string id)
    {
        var user = await _context.MyUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return user == null ? null : _mapper.Map<MyUserDto>(user);
    }

    public async Task<MyUserDto> UpdateAsync(UserUpdateDto dto)
    {
        var user = await _context.MyUsers
            .FirstOrDefaultAsync(u => u.Id == dto.Id);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
        {
            if (await _context.MyUsers.AnyAsync(u => u.Email == dto.Email))
                throw new InvalidOperationException("Email already in use.");
            user.Email = dto.Email;
        }

        if (!string.IsNullOrEmpty(dto.Password))
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<MyUserDto>(user);
    }
}