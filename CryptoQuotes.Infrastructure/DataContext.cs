using System;
using System.Threading.Tasks;
using CryptoQuotes.Background.Entities;
using CryptoQuotes.Background.Entities.RepeatingTask;
using CryptoQuotes.Infrastructure.Identity;
using Domain;
using Domain.Entities.AppUser;
using Domain.Entities.Cryptocurrency;
using Domain.Entities.CryptoQuote;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace CryptoQuotes.Infrastructure
{
    public class DataContext : IdentityDbContext<IdentityAppUser, IdentityRole<int>, int>, IUnitOfWorkFactory
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<IdentityAppUser> IdentityAppUsers { get; set; }
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<CryptoQuote> CryptoQuotes { get; set; }
        public DbSet<RepeatingTask> RepeatingTasks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        
        public IUnitOfWork Create()
        {
            return new UnitOfWork(this, Database.BeginTransaction());
        }

        internal class UnitOfWork : IUnitOfWork
        {
            private const string SavepointName = "savepoint";

            private readonly DataContext _dbContext;
            private readonly IDbContextTransaction _transaction;

            public UnitOfWork(DataContext dbContext, IDbContextTransaction transaction)
            {
                _dbContext = dbContext;
                _transaction = transaction;
            }

            private async Task SetTransactionSavepoint()
            {
                var txn = (NpgsqlTransaction) _transaction.GetDbTransaction();
                await txn.SaveAsync(SavepointName);
            }

            private async Task RollbackToSavepoint()
            {
                var txn = (NpgsqlTransaction) _transaction.GetDbTransaction();
                await txn.RollbackAsync(SavepointName);
            }

            private async Task SaveInternal(Func<Task> afterSaveAction)
            {

                await SetTransactionSavepoint();
                try
                {
                    await _dbContext.SaveChangesAsync();

                    // saving successful - call afterSaveAction
                    if (afterSaveAction != null)
                        await afterSaveAction();
                }
                catch
                {
                    await RollbackToSavepoint();
                    throw;
                }
            }

            public Task Save()
                => SaveInternal(null);

            public Task Apply()
                => SaveInternal(() => _transaction.CommitAsync());
            
            public Task Cancel()
            {
                return _transaction.RollbackAsync();
            }

            public void Dispose()
            {
                _transaction.Dispose();
            }
        }
    }

}