using NUnit.Framework;

namespace decomplex.Tests
{
    /// <summary>
    /// Base class for a BDD test.
    /// </summary>
    [TestFixture]
    public abstract class BddTestBase
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        protected virtual void Given() { }
        protected virtual void When() { }
    }
}