using Identity.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedProject;

namespace Identity.Application.User.Queries.FilterUsers;

public record FilterUserQueryHandler(
    IUserRepository userRepository) :
    IRequestHandler<FilterUserQuery, Result<FilterUsersDto>>
{
    public async Task<Result<FilterUsersDto>> Handle(FilterUserQuery request, CancellationToken cancellationToken)
    {
        var query = userRepository.GetAllQueryable();


        if (!string.IsNullOrEmpty(request.filter.Mobile))
            query = query.Where(s => s.PhoneNumber != null && EF.Functions.Like(s.PhoneNumber, $"%{request.filter}%"));

        await request.filter.Paging(query);

        return Result<FilterUsersDto>.Success(request.filter);
    }
}
