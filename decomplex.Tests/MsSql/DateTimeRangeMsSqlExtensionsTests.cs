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
        private DateTime dateFrom = new DateTime(2016, 11, 22, 9, 45, 59, 111);
        private DateTime dateTo = new DateTime(2016, 11, 23, 16, 58, 11, 222);
        private const string dateFromMsSqlLiteral = "'2016-11-22 09:45:59.111'";
        private const string dateToMsSqlLiteral = "'2016-11-23 16:58:11.222'";
        private const string columnDate = "fooDate";

        [Test]
        public void GetMsSqlCondition_FromToInclusive_ShouldReturnBetweenAnd()
        {
            var range = new OneDayRange(DateTime.Now);

            var expected = $"{columnDate} BETWEEN {dateFromMsSqlLiteral} AND {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(dateFrom, dateTo, true).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_FromToExclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= {dateFromMsSqlLiteral} AND {columnDate} < {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(dateFrom, dateTo, false).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyToInclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} <= {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(DateTime.MinValue, dateTo, true).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyToExclusive_ShouldReturnExpected()
        {
            var expected = $"{columnDate} < {dateToMsSqlLiteral}";
            var result = CreateDateTimeRange(DateTime.MinValue, dateTo, false).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_OnlyFrom_ShouldReturnExpected()
        {
            var expected = $"{columnDate} >= {dateFromMsSqlLiteral}";
            var result = CreateDateTimeRange(dateFrom, DateTime.MaxValue, false).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetMsSqlCondition_NoFromNorTo_ShouldReturnExpected()
        {
            var expected = $"{columnDate} IS NOT NULL";
            var result = CreateDateTimeRange(DateTime.MinValue, DateTime.MaxValue, false).GetMsSqlCondition(columnDate);

            Assert.AreEqual(expected, result);
        }

        private IDateTimeRange CreateDateTimeRange(DateTime from, DateTime to, bool inclusiveTo)
        {
            var dateTimeRange = MockRepository.GenerateMock<IDateTimeRange>();
            dateTimeRange.Stub(x => x.From).Return(from);
            dateTimeRange.Stub(x => x.To).Return(to);
            dateTimeRange.Stub(x => x.InclusiveTo).Return(inclusiveTo);
            return dateTimeRange;
        }
    }
}