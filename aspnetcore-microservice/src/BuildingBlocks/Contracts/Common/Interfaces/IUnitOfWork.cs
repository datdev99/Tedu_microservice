using Microsoft.EntityFrameworkCore;

namespace Contracts.Common.Interfaces;

public interface IUnitOfWork<TContext> 
    where TContext : DbContext
{
    Task<int> CommitAsync();
    void Dispose();
}