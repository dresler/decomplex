using System;
using System.Collections.Generic;
using decomplex.Parsers;
using NUnit.Framework;

namespace decomplex.Tests
{
    [TestFixture]
    public class StringParametersParserTests
    {
        [Test]
        public void Parse_ForNullString_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StringParametersParser.Parse(null));
        }

        [Test]
        public void Parse_ForEmptyString_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => StringParametersParser.Parse(String.Empty));
        }

        [Test]
        public void Parse_ForEmptyPairs_ShouldNotThrowException()
        {
            Assert.DoesNotThrow(() => StringParametersParser.Parse("|Key=Value||"));
        }

        [Test]
        public void Parse_ForDuplicateKeys_ShouldThrowParseException()
        {
            Assert.Throws<FormatException>(() => StringParametersParser.Parse("Key1=ValueA|Key1=ValueB"));
        }

        public class When_OneCorrectParameter : BddTestBase
        {
            private IDictionary<string, string> _parameters;

            protected override void When()
            {
                _parameters = StringParametersParser.Parse("Key1=Value1");
            }

            [Then]
            public void It_Should_Contain_Expected_Normal_Key()
            {
                Assert.IsTrue(_parameters.ContainsKey("Key1"));
            }

            [Then]
            public void It_Should_Contain_Expected_Uppercase_Key()
            {
                Assert.IsTrue(_parameters.ContainsKey("KEY1"));
            }

            [Then]
            public void It_Should_Contain_Expected_Lowercase_Key()
            {
                Assert.IsTrue(_parameters.ContainsKey("key1"));
            }

            [Then]
            public void It_Should_Return_Expected_Value()
            {
                Assert.AreEqual("Value1", _parameters["Key1"]);
            }
        }

        public class When_ParameterWithoutValue : BddTestBase
        {
            private IDictionary<string, string> _parameters;

            protected override void When()
            {
                _parameters = StringParametersParser.Parse("Key=");
            }

            [Then]
            public void It_Should_Contain_Key()
            {
                Assert.IsTrue(_parameters.ContainsKey("Key"));
            }

            [Then]
            public void It_Should_Return_EmptyValue()
            {
                Assert.AreEqual(string.Empty, _parameters["Key"]);
            }
        }

        public class When_TwoCorrectParameters : BddTestBase
        {
            private IDictionary<string, string> _parameters;

            protected override void When()
            {
                _parameters = StringParametersParser.Parse("Key1=Value1|Key2=Value2");
            }

            [Then]
            public void It_Should_Contain_Key1()
            {
                Assert.IsTrue(_parameters.ContainsKey("Key1"));
            }

            [Then]
            public void It_Should_Contain_Key2()
            {
                Assert.IsTrue(_parameters.ContainsKey("Key2"));
            }
            
            [Then]
            public void It_Should_Return_Expected_Value_For_Key1()
            {
                Assert.AreEqual("Value1", _parameters["Key1"]);
            }

            [Then]
            public void It_Should_Return_Expected_Value_For_Key2()
            {
                Assert.AreEqual("Value2", _parameters["Key2"]);
            }
        }
    }
}