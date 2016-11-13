using System;
using decomplex.MsSql;
using decomplex.Range;
using NUnit.Framework;

namespace decomplex.Tests.MsSql
{
    [TestFixture]
    public class DateTimeRangeIntegrationTests
    {
        private const string columnDate = "fooDate";
        
        [Test,Category("Integration")]
        public void MsSqlCondition_ForOneDayRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= '2016-01-15 00:00:00.000' AND {columnDate} < '2016-01-16 00:00:00.000'";
            var range = new OneDayRange(new Date(2016, 1, 15));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForBeforeDateRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} < '2016-01-15 00:00:00.000'";
            var range = new BeforeDateRange(new Date(2016, 1, 15));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForToDateRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} < '2016-01-16 00:00:00.000'";
            var range = new ToDateRange(new Date(2016, 1, 15));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForAfterDateRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= '2016-01-16 00:00:00.000'";
            var range = new AfterDateRange(new Date(2016, 1, 15));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForFromDateRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= '2016-01-15 00:00:00.000'";
            var range = new FromDateRange(new Date(2016, 1, 15));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForFromToDateRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= '2016-01-15 00:00:00.000' AND {columnDate} < '2016-02-23 00:00:00.000'";
            var range = new FromToDateRange(new Date(2016, 1, 15), new Date(2016, 2, 22));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test,Category("Integration")]
        public void MsSqlCondition_ForNonZeroTimeToRange_ShouldReturnExpected()
        {
            var expected = $"{columnDate} BETWEEN '2016-01-15 07:15:55.001' AND '2016-02-23 18:05:11.987'";
            var range = new NonZeroTimeToRange(new DateTime(2016, 1, 15, 7, 15, 55, 1), new DateTime(2016, 2, 23, 18, 5, 11, 987));

            var result = range.GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }
    }
}