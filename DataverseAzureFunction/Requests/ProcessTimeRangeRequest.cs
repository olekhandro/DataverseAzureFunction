using System;

namespace DataverseAzureFunction.Requests
{
    internal class ProcessTimeRangeRequest
    {
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
    }
}
