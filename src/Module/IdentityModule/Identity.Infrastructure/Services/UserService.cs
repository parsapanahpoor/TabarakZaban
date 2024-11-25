using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Domain;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Framework.Infrastructure.Shared.TokenOption;
using Identity.Application;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Services;

public class UserService(
	IHttpContextAccessor httpContextAccessor,
	UserManager<ApplicationUser> userManager,
	IOptions<ApplicationTokenOption> tokenOptions,
	IUserStore<ApplicationUser> userStore,
	IOptions<IdentityOptions> identityOptions,
	IPasswordHasher<ApplicationUser> passwordHasher,
	IEnumerable<IUserValidator<ApplicationUser>> userValidators,
	IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
	ILookupNormalizer keyNormalizer,
	IdentityErrorDescriber errors,
	IServiceProvider services,
	ILogger<UserManager<ApplicationUser>> logger)
	: base(userStore, identityOptions, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
{
	_httpContextAccessor = httpContextAccessor;
	_userManager = userManager;
	_tokenOptions = tokenOptions.Value;
}

public class UserService : UserManager<ApplicationUser>  , IUserService 
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationTokenOption _tokenOptions;

    public UserService(IHttpContextAccessor httpContextAccessor ,
        UserManager<ApplicationUser> userManager ,
        IOptions<ApplicationTokenOption> tokenOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _tokenOptions = tokenOptions.Value;
    }

    public async Task<ApplicationUser?> GetCurrentUser(CancellationToken cancellationToken)
        => await _userManager.FindByNameAsync(GetCurrentUsername());

    public string? GetCurrentUsername()
        => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true
                   ? _httpContextAccessor.HttpContext.User.Identity.Name
                   : null;

    public bool HasChart(string username, int chartId)
        => throw new NotImplementedException();

    public bool HasRole(string username, string role)
        => throw new NotImplementedException();

    public string GenerateAccessToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var keyBytes = Encoding.UTF8.GetBytes(_tokenOptions.Key!);
        var signingKey = new SymmetricSecurityKey(keyBytes);
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<string>? GetCurrentUserId(CancellationToken cancellationToken)
        => (await GetCurrentUser(cancellationToken)).Id;
}

