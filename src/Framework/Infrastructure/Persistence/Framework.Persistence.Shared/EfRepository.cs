using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Domain.Abstraction;
using Framework.Domain.Abstraction.ReadParameters;
using Microsoft.EntityFrameworkCore;
using Framework.Persistence.Shared.Extensions;
using Framework.Application.Abstraction;

namespace Framework.Persistence.Shared;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> DbSet;
    private readonly IMapper _mapper;

    public EfRepository(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = unitOfWork as DbContext ?? throw new InvalidCastException();
        DbSet = _context.Set<TEntity>();
        _mapper = mapper;
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(wherePredicate, cancellationToken);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate)
    {
        return DbSet.FirstOrDefault(wherePredicate);
    }

    public async Task<List<TEntity>> GetAllAsync(IQueryParameter queryParameter, CancellationToken cancellationToken)
    {
        var query = ApplyParameterToQuery(DbSet, queryParameter);
        return await query.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        return await query.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken)
    {
        return await DbSet.AsNoTrackingWithIdentityResolution().Where(wherePredicate).ToListAsync(cancellationToken);
    }

    public async Task<List<TResult>> GetAllAsync<TResult>(IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        return await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public async Task<List<TResult>> GetAllAsync<TResult>(IQueryParameter queryParameter, CancellationToken cancellationToken)
    {
        var query = ApplyParameterToQuery(DbSet, queryParameter);
        return await GetAllAsync<TResult>(query, cancellationToken);
    }

    public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken)
    {
        var query = DbSet.AsNoTrackingWithIdentityResolution().Where(wherePredicate);
        return await GetAllAsync<TResult>(query, cancellationToken);
    }

    public IQueryable<TEntity> GetAllQueryable()
    {
        return DbSet.AsNoTracking().AsQueryable();
    }

    public async Task<int> GetTotalAsync(IQueryParameter queryParameter, CancellationToken cancellationToken)
    {
        return await DbSet.AsNoTrackingWithIdentityResolution().CountAsync(cancellationToken);
    }

    public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, TResult>> selectPredicate, CancellationToken cancellationToken)
    {
        return await DbSet.AsNoTrackingWithIdentityResolution().Where(wherePredicate).Select(selectPredicate).ToListAsync(cancellationToken);
    }

    public async Task<TResult?> GetAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TResult>> selectPredicate,
        CancellationToken cancellationToken)
    {
        return await DbSet.Where(wherePredicate).Select(selectPredicate).FirstOrDefaultAsync(cancellationToken);
    }

    protected IQueryable<T> ApplyParameterToQuery<T>(IQueryable<T> query, IQueryParameter queryParameter)
    {
        if (queryParameter.Sort?.Any() == true)
            query = query.ApplyOrderBy(queryParameter.Sort);

        if (queryParameter.Skip is not null and not 0)
            query = query.Skip(queryParameter.Skip.Value);

        if (queryParameter.Take is not null and not 0)
            query = query.Take(queryParameter.Take.Value);

        return query;
    }

    public async Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task<bool> IsExist(Expression<Func<TEntity, bool>> anyPredicate, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(anyPredicate, cancellationToken);
    }

    public async Task<bool> IsExist<TKey>(TKey id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken) is not null;
    }

    public async Task<TEntity?> FindByIdAsync<TKey>(TKey id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void RemoveRange(params TEntity[] entities)
    {
        DbSet.RemoveRange(entities);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}