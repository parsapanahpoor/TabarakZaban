using MediatR;
using SharedProject;

namespace Identity.Application.User.Queries.FilterUsers;

public record FilterUserQuery(FilterUsersDto filter) : 
    IRequest<Result<FilterUsersDto>>;
