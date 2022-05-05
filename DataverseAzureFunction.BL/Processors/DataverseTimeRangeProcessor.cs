using System;
using System.Threading.Tasks;
using DataverseAzureFunction.Core.Interfaces.BL;
using DataverseAzureFunction.Core.Interfaces.DAL;

namespace DataverseAzureFunction.BL.Processors
{
    public class DataverseTimeRangeProcessor : IDataverseTimeRangeProcessor
    {
        private readonly IDataverseTimeEntryManager _manager;

        public DataverseTimeRangeProcessor(IDataverseTimeEntryManager manager)
        {
            _manager = manager;
        }

        public async Task ProcessTimeRangeAsync(DateTime startDate, DateTime endDate)
        {
            var currentDate = startDate;
            while (currentDate < endDate)
            {
                currentDate = currentDate.AddDays(1);
            }
            throw new NotImplementedException();
        }
    }
}
