using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace decomplex.Tests
{
    [TestFixture]
    public class ExpiringCachedContentBaseTests
    {
        private const int MaxAgeInMilliseconds = 100;

        [Test]
        public void Ctor_ShouldNotAskFoo()
        {
            var foo = MockRepository.GenerateStub<IFoo>();
            var dateTimeNowProvider = MockRepository.GenerateStub<IDateTimeNowProvider>();

            new ExpiringCachedContentTest(dateTimeNowProvider, foo);

            foo.AssertWasNotCalled(x => x.GetValue());
        }

        [Test]
        public void Get_ForFirstTime_ShouldAskFooOnce()
        {
            var foo = MockRepository.GenerateStub<IFoo>();
            var dateTimeNowProvider = MockRepository.GenerateStub<IDateTimeNowProvider>();

            var currentDateTime = DateTime.Now;

            dateTimeNowProvider
                .Stub(x => x.Now)
                .Do((Func<DateTime>)(() => currentDateTime));

            var underTest = new ExpiringCachedContentTest(dateTimeNowProvider, foo);

            underTest.GetValue();

            foo.AssertWasCalled(x => x.GetValue(), options => options.Repeat.Once());
        }

        [Test]
        public void Get_ForSecondTimeBeforeMaxAge_ShouldAskFooOnce()
        {
            var foo = MockRepository.GenerateStub<IFoo>();
            var dateTimeNowProvider = MockRepository.GenerateStub<IDateTimeNowProvider>();

            var currentDateTime = DateTime.Now;

            dateTimeNowProvider
                .Stub(x => x.Now)
                .Do((Func<DateTime>) (() => currentDateTime));

            var underTest = new ExpiringCachedContentTest(dateTimeNowProvider, foo);

            underTest.GetValue();

            currentDateTime = currentDateTime.AddMilliseconds(MaxAgeInMilliseconds - 1);

            underTest.GetValue();

            foo.AssertWasCalled(x => x.GetValue(), options => options.Repeat.Once());
        }

        [Test]
        public void Get_ForSecondTimeAfterMaxAge_ShouldAskFooTwice()
        {
            var foo = MockRepository.GenerateStub<IFoo>();
            var dateTimeNowProvider = MockRepository.GenerateStub<IDateTimeNowProvider>();

            var currentDateTime = DateTime.Now;

            dateTimeNowProvider
                .Stub(x => x.Now)
                .Do((Func<DateTime>)(() => currentDateTime));

            var underTest = new ExpiringCachedContentTest(dateTimeNowProvider, foo);

            underTest.GetValue();

            currentDateTime = currentDateTime.AddMilliseconds(MaxAgeInMilliseconds + 1);

            underTest.GetValue();

            foo.AssertWasCalled(x => x.GetValue(), options => options.Repeat.Twice());
        }

        public class ExpiringCachedContentTest : ExpiringCachedContentBase
        {
            private readonly IFoo _foo;
            private int _valueFromFoo;

            public int GetValue()
            {
                return Get(() => _valueFromFoo);
            }

            public ExpiringCachedContentTest(IDateTimeNowProvider dateTimeNowProvider, IFoo foo)
                : base(dateTimeNowProvider, MaxAgeInMilliseconds)
            {
                _foo = foo;
            }

            protected override void HandleContentValidityExpired()
            {
                _valueFromFoo = _foo.GetValue();
            }
        }

        public interface IFoo
        {
            int GetValue();
        }
    }
}