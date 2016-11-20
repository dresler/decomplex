using System;
using System.Collections.Generic;
using decomplex.ChainOfResponsibility;
using NUnit.Framework;

namespace decomplex.Tests.ChainOfResponsibility
{
    [TestFixture]
    public class ChainOfResponsibilityBaseTests
    {
        [Test]
        public void Ctor_ForEmptyCollectionOfHandlers_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => new MyNumberChainOfResponsibility(new IMyNumberHandlerChainConstruction[0]));
        }

        [Test]
        public void Handle_ForOddHandlerMatchingOdd_ShouldReturnOdd()
        {
            var handlers = new IMyNumberHandlerChainConstruction[]
            {
                new OddNumberHandler()
            };

            var underTest = new MyNumberChainOfResponsibility(handlers);
            var response = underTest.Handle(1);

            Assert.AreEqual("Odd", response);
        }

        [Test]
        public void Handle_ForOddHandlerMatchingEven_ShouldReturnDefaultResponse()
        {
            var handlers = new IMyNumberHandlerChainConstruction[]
            {
                new OddNumberHandler()
            };

            var underTest = new MyNumberChainOfResponsibility(handlers);
            var response = underTest.Handle(2);

            Assert.AreEqual(default(string), response);
        }

        [Test]
        public void Handle_ForManyHandlersCreatedInOrderEvenOddZeroMatchingZero_ShouldReturnZero()
        {
            var handlers = new IMyNumberHandlerChainConstruction[]
            {
                new EvenNumberHandler(),
                new OddNumberHandler(),
                new ZeroNumberHandler()
            };

            var underTest = new MyNumberChainOfResponsibility(handlers);
            var response = underTest.Handle(0);

            Assert.AreEqual("Zero", response);
        }

        internal interface IMyNumberHandler : IMessageHandlerWithResponse<int, string>
        {
        }

        internal interface IMyNumberHandlerChainConstruction : IMessageHandlerWithResponseConstruction<int, string>,
            IMyNumberHandler
        {
        }

        internal class MyNumberChainOfResponsibility : ChainOfResponsibilityBase<int, string>, IMyNumberHandler
        {
            public MyNumberChainOfResponsibility(IEnumerable<IMyNumberHandlerChainConstruction> handlers)
                : base(handlers)
            {
            }
        }

        [ChainOfResponsibilityOrderIndex(2)]
        internal class OddNumberHandler : MessageHandlerWithResponseBase<int, string>,
            IMyNumberHandlerChainConstruction
        {
            protected override bool TryHandle(int message, out string response)
            {
                var isOddNumber = message % 2 == 1;

                if (isOddNumber)
                {
                    response = "Odd";
                    return true;
                }

                response = string.Empty;
                return false;
            }
        }

        [ChainOfResponsibilityOrderIndex(3)]
        internal class EvenNumberHandler : MessageHandlerWithResponseBase<int, string>,
            IMyNumberHandlerChainConstruction
        {
            protected override bool TryHandle(int message, out string response)
            {
                var isEvenNumber = message % 2 == 0;

                if (isEvenNumber)
                {
                    response = "Even";
                    return true;
                }

                response = string.Empty;
                return false;
            }
        }

        [ChainOfResponsibilityOrderIndex(1)]
        internal class ZeroNumberHandler : MessageHandlerWithResponseBase<int, string>,
            IMyNumberHandlerChainConstruction
        {
            protected override bool TryHandle(int message, out string response)
            {
                var isZeroNumber = message == 0;

                if (isZeroNumber)
                {
                    response = "Zero";
                    return true;
                }

                response = string.Empty;
                return false;
            }
        }
    }
}