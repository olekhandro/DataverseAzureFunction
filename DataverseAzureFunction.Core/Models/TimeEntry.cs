using System;

namespace DataverseAzureFunction.Core.Models
{
    public class TimeEntry
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid Id { get; set; }
    }
}
