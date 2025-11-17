using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pharmacy.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IRepository<Employee> _employeeRepository;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IRepository<Employee> employeeRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _employeeRepository = employeeRepository;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
    {
        // Check if username already exists
        var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
        if (existingUser != null)
        {
            return null;
        }

        // Create new employee
        var employee = new Employee
        {
            Name = registerDto.EmployeeName,
            Position = registerDto.Position,
            PhoneNumber = registerDto.PhoneNumber,
            Email = registerDto.Email,
            Salary = registerDto.Salary,
            HireDate = DateTime.Now
        };

        await _employeeRepository.AddAsync(employee);

        // Create new user linked to the employee
        var user = new ApplicationUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            EmployeeId = employee.Id
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return null;
        }

        // Generate token
        return await GenerateTokenAsync(user, employee);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
        {
            return null;
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            return null;
        }

        Employee? employee = null;
        if (user.EmployeeId.HasValue)
        {
            employee = await _employeeRepository.GetByIdAsync(user.EmployeeId.Value);
        }

        return await GenerateTokenAsync(user, employee);
    }

    private async Task<AuthResponseDto> GenerateTokenAsync(ApplicationUser user, Employee? employee)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (user.EmployeeId.HasValue)
        {
            claims.Add(new Claim("EmployeeId", user.EmployeeId.Value.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(3);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            EmployeeId = user.EmployeeId,
            EmployeeName = employee?.Name,
            Expiration = expiration
        };
    }
}
