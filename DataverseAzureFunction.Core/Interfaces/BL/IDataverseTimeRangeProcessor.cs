using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataverseAzureFunction.Core.Interfaces.BL
{
    public interface IDataverseTimeRangeProcessor
    {
        Task ProcessTimeRangeAsync(DateTime startDate, DateTime endDate);
    }
}
