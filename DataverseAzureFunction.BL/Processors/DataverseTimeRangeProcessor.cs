using System;
using System.Linq;
using System.Threading.Tasks;
using DataverseAzureFunction.Core.Interfaces.BL;
using DataverseAzureFunction.Core.Interfaces.DAL;
using DataverseAzureFunction.Core.Models;

namespace DataverseAzureFunction.BL.Processors
{
    public class DataverseTimeRangeProcessor : IDataverseTimeRangeProcessor
    {
        private readonly IDataverseTimeEntryManager _manager;

        public DataverseTimeRangeProcessor(IDataverseTimeEntryManager manager)
        {
            _manager = manager;
        }

        public async Task ProcessTimeRangeAsync(DateTime startOn, DateTime endOn)
        {
            if (endOn < startOn)
            {
                throw new ArgumentException("EndOn cannot be less than StartOn");
            }

            // we use those dates since we need to check full day, not just some exact time
            var firstDate = new DateTime(startOn.Year, startOn.Month, startOn.Day);
            var secondDate = new DateTime(endOn.Year, endOn.Month, endOn.Day, 23, 59, 59);
            var entries = await _manager.SearchTimeEntriesAsync(firstDate, secondDate);
            var currentDate = firstDate;
            while (currentDate < secondDate)
            {
                // here we assume that time entry can be made within one day. however, nothing is mentioned regarding entries,
                // which range is bigger than 1 day
                if (!entries.Any(x => x.Start > currentDate && x.End < currentDate.AddDays(1)))
                {
                    var newEntry = new TimeEntry()
                    {
                        Start = currentDate.AddHours(12),
                        End = currentDate.AddHours(13)
                    };
                    await _manager.CreateTimeEntryAsync(newEntry);
                }
                currentDate = currentDate.AddDays(1);
            }
        }
    }
}
