using System;
using Domain.Abstractions;
using Domain.Abstractions.Entity;

namespace CryptoQuotes.Background.Entities.RepeatingTask
{
    public class RepeatingTask : IEntity<int>
    {
        public RepeatingTask(RepeatingTaskType type, DateTime executeDate)
        {
            Type = type;
            ExecuteDate = executeDate;
        }

        public int Id { get; }
        
        public RepeatingTaskType Type { get; private set; }
        
        public DateTime ExecuteDate { get; private set; }

        public void SetExecuteDate(DateTime dateTime)
        {
            ExecuteDate = dateTime;
        }
    }
    
    public enum RepeatingTaskType {
        ImportCryptocurrency
    }
}