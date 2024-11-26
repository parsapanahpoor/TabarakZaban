using AutoMapper;
using Framework.Application.Abstraction;
using Framework.Persistence.Shared;
using Identity.Domain.Repository;

namespace Identity.Persistence.Repository;

public class UserRepository(IUnitOfWork context, IMapper mapper) :
	EfRepository<ApplicationUser>(context, mapper),
	IUserRepository;