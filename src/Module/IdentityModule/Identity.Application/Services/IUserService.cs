namespace Identity.Application;

public interface IUserService
{
    Task<Domain.Entities.ApplicationUser> GetCurrentUser(CancellationToken cancellationToken);
    string? GetCurrentUsername();
    Task<string>? GetCurrentUserId(CancellationToken cancellationToken);
    bool HasRole(string username, string role);
    bool HasChart(string username, int chartId);
    string GenerateAccessToken(Domain.Entities.ApplicationUser user);
    string GenerateRefreshToken();
}