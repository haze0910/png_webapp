using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barista.SharedKernel.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> ExecuteSqlAsync(string sqlCmd, params object[] args);
    }
}
