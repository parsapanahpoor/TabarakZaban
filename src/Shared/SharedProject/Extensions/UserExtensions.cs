using SharedProject.StaticTools;
using System.Security.Claims;
using System.Security.Principal;


namespace SharedProject.Application.Extensions
{
	public static class UserExtensions
    {
        public static ulong GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);

            return ulong.Parse(data.Value);
        }

        public static ulong GetUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name).Value;

            return data.ToString();
        }

        public static string GetUsername(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUsername();
        }

        public static string GetUserAvatar(this string? userAvatar)
        {
            if (!string.IsNullOrEmpty(userAvatar))
                return Path.Combine(PathTools.UserAvatarPathThumb, userAvatar);

            return PathTools.DefaultUserAvatar;
        }
    }
}
