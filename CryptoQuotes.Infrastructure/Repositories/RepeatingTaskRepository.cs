using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoQuotes.Background.Entities;
using CryptoQuotes.Background.Entities.RepeatingTask;
using Domain.Entities.AppUser;
using Microsoft.EntityFrameworkCore;
using CryptoQuotes.Infrastructure.Identity;
using Domain.Abstractions;

namespace CryptoQuotes.Infrastructure.Repositories
{
    internal class RepeatingTaskRepository: RepositoryBase<RepeatingTask>, IRepeatingTaskRepository
    {
        public RepeatingTaskRepository(DataContext dbContext) : base(dbContext)
        { }

        public async Task<RepeatingTask> GetByTypeLatest(RepeatingTaskType repeatingTaskType)
        {
            return await GetQuery().OrderByDescending(t => t.ExecuteDate)
                .FirstOrDefaultAsync(t => t.Type == repeatingTaskType);
        }
    }
}