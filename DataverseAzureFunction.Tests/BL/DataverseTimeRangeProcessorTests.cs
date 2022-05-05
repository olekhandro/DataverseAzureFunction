using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataverseAzureFunction.BL.Processors;
using DataverseAzureFunction.Core.Interfaces.BL;
using DataverseAzureFunction.Core.Interfaces.DAL;
using DataverseAzureFunction.Core.Models;
using DataverseAzureFunction.DAL.Managers;
using NUnit.Framework;

namespace DataverseAzureFunction.Tests.BL
{
    [TestFixture]
    internal class DataverseTimeRangeProcessorTests
    {
        private IDataverseTimeRangeProcessor _dataverseTimeEntryProcessor;
        private Guid _timeEntryToDeleteGuid;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var dataverseTimeEntryManager = new DataverseTimeEntryManager(AppConstants.CONNECTIONSTRING);
            _dataverseTimeEntryProcessor = new DataverseTimeRangeProcessor(dataverseTimeEntryManager);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {

        }
    }
}
