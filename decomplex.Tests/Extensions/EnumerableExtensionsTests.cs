﻿using System;
using System.Collections.Generic;
using System.Linq;
using decomplex.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace decomplex.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void ForEach_ForNullCollection_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IEnumerable<int>).ForEach(x => { }));
        }

        [Test]
        public void ForEach_ForNullAction_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<int>().ForEach(null));
        }

        [Test]
        public void ForEach_ForCollection_ShouldInvokeEachItem()
        {
            var numbers = new[] {4, 7, 1, 5};
            var iteratedNumbers = new List<int>();

            numbers.ForEach(number => iteratedNumbers.Add(number));

            iteratedNumbers.Should().Equal(numbers);
        }

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

        [Test]
        public void ToReadOnlyCollection_ForACollection_ShouldReturnExpectedType()
        {
            IEnumerable<int> collection = new[] {1, 2, 3};

            var result = collection.ToReadOnlyCollection();

            Assert.IsInstanceOf<IReadOnlyCollection<int>>(result);
        }

        [Test]
        public void ToReadOnlyCollection_ForNullCollection_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IEnumerable<int>).ToReadOnlyCollection());
        }
    }
}