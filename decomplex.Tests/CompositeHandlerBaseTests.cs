using System;
using System.Collections.Generic;
using decomplex.Mapper;
using NUnit.Framework;
using Rhino.Mocks;

namespace decomplex.Tests
{
    [TestFixture]
    public class CompositeHandlerBaseTests
    {
        [Test]
        public void Handle_ForOneParticularHandlerThatIsFor_ShouldInvokeItsHandle()
        {
            var handlerFoo = MockRepository.GenerateStub<ITestHandler>();
            handlerFoo.Stub(h => h.IsFor(MyEnum.Foo)).Return(true);

            var fooHandlerWasInvoked = false;
            handlerFoo.Stub(h => h.Handle(Arg<MyEnum>.Is.Anything)).WhenCalled(_ => fooHandlerWasInvoked = true);

            var handler = new TestCompositeHandler(new [] { handlerFoo });

            handler.Handle(MyEnum.Foo);

            Assert.IsTrue(fooHandlerWasInvoked);
        }

        [Test]
        public void Handle_ForOneParticularHandlerThatIsNotFor_ShouldThrowArgumentException()
        {
            var handlerFoo = MockRepository.GenerateStub<ITestHandler>();
            handlerFoo.Stub(h => h.IsFor(MyEnum.Foo)).Return(true);

            var handler = new TestCompositeHandler(new [] { handlerFoo });

            Assert.Throws<ArgumentException>(() => handler.Handle(MyEnum.Bar));
        }

        [Test]
        public void Handle_ForTwoParticularHandlersThatBothAreFor_ShouldHandleBoth()
        {
            var handlerFooA = MockRepository.GenerateStub<ITestHandler>();
            handlerFooA.Stub(h => h.IsFor(MyEnum.Foo)).Return(true);

            var fooHandlerAWasInvoked = false;
            handlerFooA.Stub(h => h.Handle(Arg<MyEnum>.Is.Anything)).WhenCalled(_ => fooHandlerAWasInvoked = true);

            var handlerFooB = MockRepository.GenerateStub<ITestHandler>();
            handlerFooB.Stub(h => h.IsFor(MyEnum.Foo)).Return(true);

            var fooHandlerBWasInvoked = false;
            handlerFooB.Stub(h => h.Handle(Arg<MyEnum>.Is.Anything)).WhenCalled(_ => fooHandlerBWasInvoked = true);

            var handler = new TestCompositeHandler(new [] { handlerFooA, handlerFooB });
            
            handler.Handle(MyEnum.Foo);

            // TODO dresler: Separate the asserts
            Assert.IsTrue(fooHandlerAWasInvoked);
            Assert.IsTrue(fooHandlerBWasInvoked);
        }

        public class TestCompositeHandler : CompositeHandlerBase<MyEnum, ITestHandler>
        {
            public TestCompositeHandler(IEnumerable<ITestHandler> handlers) : base(handlers)
            {
            }
        }

        public interface ITestHandler : IHandler<MyEnum> { }

        public enum MyEnum
        {
            Foo,
            Bar
        }
    }
}