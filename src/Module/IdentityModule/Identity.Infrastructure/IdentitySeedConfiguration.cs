using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedProject;

namespace Identity.Infrastructure;

public class IdentitySeedConfiguration
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await SeedUsers(userManager, roleManager);
    }

    private static async Task SeedUsers(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        var user = await CreateDefaultUser(userManager, Constants.AdminEmail, "123456");
        var role = await CreateDefaultRole(roleManager, Constants.AdminRole);

        if (user is not null && role is not null)
            userManager.AddToRoleAsync(user, role.Name!).Wait();
    }

    private static async Task<ApplicationUser?> CreateDefaultUser(UserManager<ApplicationUser> userManager, string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
            return user;

        var result = await userManager.CreateAsync(new ApplicationUser() {Email = email}, password);
        return result.Succeeded ? await userManager.FindByEmailAsync(email) : null!;
    }

    private static async Task<ApplicationRole?> CreateDefaultRole(RoleManager<ApplicationRole> roleManager, string roleName)
    {
        var role = (await roleManager.FindByNameAsync(roleName));
        if (role is not null)
            return role;

        var result = await roleManager.CreateAsync(new ApplicationRole() {Name = roleName});
        return result.Succeeded ? roleManager.FindByNameAsync(roleName).Result : null!;
    }
}