using System;
using decomplex.MsSql;
using decomplex.Range;
using NUnit.Framework;
using Rhino.Mocks;

namespace decomplex.Tests.MsSql
{
    [TestFixture]
    public class DateTimeRangeMsSqlExtensionsTests
    {
        private DateTime dateTimeFrom = new DateTime(2016, 11, 22, 9, 45, 59, 111);
        private Date dateFrom = new Date(2016, 11, 22);

        private DateTime dateTimeTo = new DateTime(2016, 11, 23, 16, 58, 11, 222);
        private Date dateTo = new Date(2016, 11, 23);

        private const string dateTimeFromMsSqlLiteral = "'2016-11-22 09:45:59.111'";
        private const string dateFromMsSqlLiteral = "'2016-11-22 00:00:00.000'";

        private const string dateTimeToMsSqlLiteral = "'2016-11-23 16:58:11.222'";
        private const string dateToMsSqlLiteral = "'2016-11-23 00:00:00.000'";

        private const string columnDate = "fooDate";

        [Test]
        public void GetMsSqlCondition_FromToInclusive_ShouldReturnBetweenAnd()
        {
            var expected = $"{columnDate} BETWEEN {dateTimeFromMsSqlLiteral} AND {dateTimeToMsSqlLiteral}";
            var result = CreateDateTimeRange(dateTimeFrom, dateTimeTo).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_FromToExclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= {dateFromMsSqlLiteral} AND {columnDate} < {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(dateFrom, dateTo).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyToInclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} <= {dateTimeToMsSqlLiteral}";
            var result = CreateDateTimeRange(DateTime.MinValue, dateTimeTo).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyToExclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} < {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(DateTime.MinValue, dateTo).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyFrom_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= {dateTimeFromMsSqlLiteral}";
            var result = CreateDateTimeRange(dateTimeFrom, DateTime.MaxValue).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_NoFromNorTo_ShouldReturnExpected()
        {
            var expected = $"{columnDate} IS NOT NULL";
            var result = CreateDateTimeRange(DateTime.MinValue, DateTime.MaxValue).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        private IDateTimeRange CreateDateTimeRange(DateTime from, DateTime to)
        {
            var dateTimeRange = MockRepository.GenerateMock<IDateTimeRange>();
            dateTimeRange.Stub(x => x.From).Return(from);
            dateTimeRange.Stub(x => x.To).Return(to);
            return dateTimeRange;
        }
    }
}