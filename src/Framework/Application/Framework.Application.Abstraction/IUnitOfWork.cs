namespace Framework.Application.Abstraction;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{ 
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
    void Attach<TEntity>(TEntity entity);
}