using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}