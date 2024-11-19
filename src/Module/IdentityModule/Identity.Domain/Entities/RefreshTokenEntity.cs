
using Framework.Domain.Shared;

namespace Identity.Domain.Entities;

public class RefreshTokenEntity : Entity<Guid>
{
    public string? Token { get; set; }
    public DateTime IssuedDateTime { get; set; }
    public DateTime ExpirationDateTime { get; set; }
    public bool IsExpired { get; set; } = false;
    
    public virtual ApplicationUser? User { get; set; }
    public string? UserId { get; set; }
}
