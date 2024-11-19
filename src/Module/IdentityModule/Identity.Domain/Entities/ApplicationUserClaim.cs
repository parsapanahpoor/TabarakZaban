using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}