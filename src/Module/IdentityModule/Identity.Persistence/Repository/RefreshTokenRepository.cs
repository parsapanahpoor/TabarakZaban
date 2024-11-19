using AutoMapper;
using Framework.Application.Abstraction;
using Framework.Persistence.Shared;
using Identity.Domain.Repository;

namespace Identity.Persistence.Repository;

public class RefreshTokenRepository(IUnitOfWork context, IMapper mapper) :
    EfRepository<RefreshTokenEntity>(context, mapper),
    IRefreshTokenRepository;