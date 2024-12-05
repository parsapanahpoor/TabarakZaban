using Framework.Domain.Shared.DTO.Common;
using Identity.Domain.Entities;

namespace Identity.Application.User.Queries.FilterUsers;

public class FilterUsersDto : BasePaging<ApplicationUser>
{
    public string? Mobile { get; set; }
}
