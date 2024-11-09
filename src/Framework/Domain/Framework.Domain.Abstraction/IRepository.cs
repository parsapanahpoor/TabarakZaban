using System.Linq.Expressions;
using Framework.Domain.Abstraction.ReadParameters;

namespace Framework.Domain.Abstraction;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate);

    public Task<List<TEntity>> GetAllAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(IQueryParameter queryParameter, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken);

    public Task<List<TResult>> GetAllAsync<TResult>(IQueryable<TEntity> query, CancellationToken cancellationToken);
    public Task<List<TResult>> GetAllAsync<TResult>(IQueryParameter queryParameter, CancellationToken cancellationToken);
    public Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken);

    public Task<int> GetTotalAsync(IQueryParameter queryParameter, CancellationToken cancellationToken);
    
    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, TResult>> selectPredicate, CancellationToken cancellationToken);

    Task<TResult?> GetAsync<TResult>(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TResult>> selectPredicate,
        CancellationToken cancellationToken);

    Task<bool> IsExist(Expression<Func<TEntity, bool>> anyPredicate, CancellationToken cancellationToken);
    public Task<bool> IsExist<TKey>(TKey id, CancellationToken cancellationToken);

    Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken);
    Task<TEntity?> FindByIdAsync<TKey>(TKey id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    void RemoveRange(params TEntity[] entities);

    Task SaveAsync();
}