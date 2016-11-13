using System;
using decomplex.Range;
using NUnit.Framework;

namespace decomplex.Tests.Range
{
    [TestFixture]
    public class NonZeroTimeToRangeTests
    {
        [Test]
        public void Ctor_ForZeroToTime_ShouldReturnArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new NonZeroTimeToRange(DateTime.Now.AddDays(-1), DateTime.Now.Date));
        }
    }
}