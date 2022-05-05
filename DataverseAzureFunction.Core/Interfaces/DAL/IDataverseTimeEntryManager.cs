using DataverseAzureFunction.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataverseAzureFunction.Core.Interfaces.DAL
{
    public interface IDataverseTimeEntryManager
    {
        Task<IEnumerable<TimeEntry>> GetTimeEntriesAsync();
        Task<IEnumerable<TimeEntry>> SearchTimeEntriesAsync(DateTime startDate, DateTime endDate);
        Task<Guid> CreateTimeEntryAsync(TimeEntry timeEntry);
        Task DeleteTimeEntryAsync(Guid timeEntryId);
    }
}
