using System;
using System.Linq;
using System.Threading.Tasks;
using DataverseAzureFunction.Core.Interfaces.DAL;
using DataverseAzureFunction.Core.Models;
using DataverseAzureFunction.DAL.Managers;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace DataverseAzureFunction.Tests.DAL
{
    [TestFixture]
    public class DataverseTimeEntryManagerTests
    {
        private IDataverseTimeEntryManager _dataverseTimeEntryManager;
        private Guid _timeEntryToDeleteGuid;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _dataverseTimeEntryManager = new DataverseTimeEntryManager(AppConstants.CONNECTIONSTRING);
            var timeEntry = new TimeEntry()
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };
            _timeEntryToDeleteGuid = await _dataverseTimeEntryManager.CreateTimeEntryAsync(timeEntry);
        }

        [Test]
        public async Task CreateTimeEntryAsyncTest()
        {
            var timeEntry = new TimeEntry()
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };
            var result = await _dataverseTimeEntryManager.CreateTimeEntryAsync(timeEntry);
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetTimeEntriesAsyncTest()
        {
            var result = await _dataverseTimeEntryManager.GetTimeEntriesAsync();
            Assert.Greater(result.Count(), 0);
        }

        [Test]
        public async Task SearchTimeEntriesAsync()
        {
            var result = await _dataverseTimeEntryManager.SearchTimeEntriesAsync(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.Greater(result.Count(), 0);
        }

        [Test]
        public async Task DeleteTimeEntryAsyncTest()
        {
            await _dataverseTimeEntryManager.DeleteTimeEntryAsync(_timeEntryToDeleteGuid);
            var timeentries = await _dataverseTimeEntryManager.GetTimeEntriesAsync();
            Assert.False(timeentries.Any(x=>x.Id == _timeEntryToDeleteGuid));
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            var timeentries = await _dataverseTimeEntryManager.GetTimeEntriesAsync();
            foreach (var timeentry in timeentries)
            {
                await _dataverseTimeEntryManager.DeleteTimeEntryAsync(timeentry.Id);
            }
        }
    }
}
