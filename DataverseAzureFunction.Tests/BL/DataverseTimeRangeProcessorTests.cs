using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// used to check data which should appear in database after test runs
        /// </summary>
        private IDataverseTimeEntryManager _dataverseTimeEntryManager;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _dataverseTimeEntryManager= new DataverseTimeEntryManager(AppConstants.CONNECTIONSTRING);
            _dataverseTimeEntryProcessor = new DataverseTimeRangeProcessor(_dataverseTimeEntryManager);
        }

        [Test]
        public async Task WrongTimeRangeAsyncTest()
        {
            var firstDate = DateTime.Today;
            var secondDate = DateTime.Today.AddDays(-1);
            Assert.ThrowsAsync(typeof(ArgumentException), () => _dataverseTimeEntryProcessor.ProcessTimeRangeAsync(firstDate, secondDate));
        }

        [Test]
        public async Task TimeRangeGenerationAsyncTest()
        {
            var firstDate = DateTime.Today.AddHours(10);
            var secondDate = DateTime.Today.AddHours(34);
            await _dataverseTimeEntryProcessor.ProcessTimeRangeAsync(firstDate, secondDate);
            var entries = await _dataverseTimeEntryManager.GetTimeEntriesAsync();
            Assert.AreEqual(2, entries.Count());
        }


        [Test]
        public async Task TimeRangeGenerationWithExistingEntriesAsyncTest()
        {
            var baseDate = DateTime.Today.AddDays(7);
            var firstDate = baseDate.AddHours(10);
            var secondDate = firstDate.AddDays(4);
            var existingEntryDate = firstDate.AddDays(1);

            var existingEntryGuid = await _dataverseTimeEntryManager.CreateTimeEntryAsync(new TimeEntry()
            {
                Start = existingEntryDate,
                End = existingEntryDate.AddHours(1)
            });
            Assert.AreNotEqual(new Guid(), existingEntryGuid);
            await _dataverseTimeEntryProcessor.ProcessTimeRangeAsync(firstDate, secondDate);
            var entries = await _dataverseTimeEntryManager.GetTimeEntriesAsync();
            Assert.AreEqual(5, entries.Count());
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
