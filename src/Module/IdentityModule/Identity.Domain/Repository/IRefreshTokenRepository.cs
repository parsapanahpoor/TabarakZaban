using Framework.Domain.Abstraction;
using Identity.Domain.Entities;

namespace Identity.Domain.Repository;

public interface IRefreshTokenRepository : 
    IRepository<RefreshTokenEntity>;
