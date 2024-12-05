using Identity.Application.User.Queries.FilterUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Admin.Controllers;

namespace WebApp.Areas.Admin.Controllers;

public class UserController(
    IMediator mediator) : AdminBaseController
{
    [HttpGet]
    public async Task<IActionResult> FilterUsers(FilterUsersDto filter , CancellationToken cancellationToken)
        => View((await mediator.Send(new FilterUserQuery(
            filter : filter) , 
            cancellationToken)).Value);
}
