namespace Identity.Domain.Entities;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public virtual ICollection<ApplicationUserClaim>? Claims { get; set; }
    public virtual ICollection<ApplicationUserLogin>? Logins { get; set; }
    public virtual ICollection<ApplicationUserToken>? Tokens { get; set; }
    public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }

    public virtual List<RefreshTokenEntity> RefreshTokens { get; set; } = [];
	public string? ActivationCode { get; set; }
    public string? UserAvatar { get; set; }
    public bool IsBan { get; set; } = false;
	public bool IsAdmin { get; set; } = false;
}