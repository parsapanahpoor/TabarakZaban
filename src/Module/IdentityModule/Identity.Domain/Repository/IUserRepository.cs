using Framework.Domain.Abstraction;
using Identity.Domain.Entities;

namespace Identity.Domain.Repository;

public interface IUserRepository :
	IRepository<ApplicationUser>;
