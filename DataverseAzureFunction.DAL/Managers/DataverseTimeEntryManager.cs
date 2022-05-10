using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataverseAzureFunction.Core.Interfaces.DAL;
using DataverseAzureFunction.Core.Models;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;

namespace DataverseAzureFunction.DAL.Managers
{
    public class DataverseTimeEntryManager : BaseDataverseManager, IDataverseTimeEntryManager
    {
        public DataverseTimeEntryManager(string connectionString) : base(connectionString)
        {
        }

        public async Task DeleteTimeEntryAsync(Guid timeEntryId)
        {
            await _serviceClient.DeleteAsync("msdyn_timeentry", timeEntryId);
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesAsync()
        {
            var entities = await _serviceClient.RetrieveMultipleAsync(new QueryExpression("msdyn_timeentry")
            {
                ColumnSet = new ColumnSet("msdyn_timeentryid", "msdyn_start", "msdyn_end")
            });
            return entities.Entities.Select(x => new TimeEntry()
            {
                Id = x.Id,
                Start = (DateTime)x["msdyn_start"],
                End = (DateTime)x["msdyn_end"]
            });
        }

        public async Task<IEnumerable<TimeEntry>> SearchTimeEntriesAsync(DateTime startDate, DateTime endDate)
        {
            var queryExpression = new QueryExpression("msdyn_timeentry");
            queryExpression.ColumnSet = new ColumnSet("msdyn_timeentryid", "msdyn_start", "msdyn_end");
            queryExpression.Criteria = new FilterExpression()
            {
                Conditions =
                {
                    new ConditionExpression()
                    {
                        AttributeName = "msdyn_start",
                        Operator = ConditionOperator.GreaterThan,
                        Values = { startDate }
                    },
                    new ConditionExpression()
                    {
                        AttributeName = "msdyn_start",
                        Operator = ConditionOperator.LessThan,
                        Values = { endDate }
                    }
                }
            }; 
            var entities = await _serviceClient.RetrieveMultipleAsync(queryExpression); 
            return entities.Entities.Select(x => new TimeEntry()
            {
                Id = x.Id,
                Start = (DateTime)x["msdyn_start"],
                End = (DateTime)x["msdyn_end"]
            });
        }

        public async Task<Guid> CreateTimeEntryAsync(TimeEntry timeEntry)
        {
            var entity = new Entity("msdyn_timeentry");
            entity["msdyn_start"] = timeEntry.Start;
            entity["msdyn_end"] = timeEntry.End;
            var result = await _serviceClient.CreateAsync(entity);
            return result;
        }
    }
}
