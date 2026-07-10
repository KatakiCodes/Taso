using Microsoft.AspNetCore.Identity;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;

namespace Taso.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<string>> RegisterUserAsync(string email, string password, string fullName)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Result<string>.Success(user.Id);
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        return Result<string>.Failure(errors);
    }

    public async Task<Result<string>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<string>.Failure(new List<string> { "Credenciais inválidas." });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {
            var token = _jwtTokenGenerator.GenerateToken(user);
            return Result<string>.Success(token);
        }

        return Result<string>.Failure(new List<string> { "Credenciais inválidas." });
    }
}
