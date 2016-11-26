using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace decomplex.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void Yield_WhenInteger_ShouldReturnIEnumerableWithTheInteger()
        {
            var result = 1.Yield();
            result.ShouldBeEquivalentTo(new [] {1});
        }

        [Test]
        public void Yield_WhenNullInteger_ShouldReturnIEnumerableWithTheNullInteger()
        {
            var result = new int?().Yield();
            result.ShouldBeEquivalentTo(new int?[] {null});
        }
    }
}