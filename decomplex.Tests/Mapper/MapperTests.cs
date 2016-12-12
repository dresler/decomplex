using System;
using System.Collections.Generic;
using decomplex.Mapper;
using NUnit.Framework;

namespace decomplex.Tests.Mapper
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void Map_ForOneKeyAndMappingKey_ShouldReturnValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(0).Return(1);

            var result = mapper.Map(0);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Map_ForOneKeyAndMappingNonexistingKey_ShouldThrowKeyNotFoundException()
        {
            var mapper = new Mapper<int, int>()
                .Where(0).Return(1);

            Assert.Throws<KeyNotFoundException>(() => mapper.Map(1));
        }

        [Test]
        public void Map_ForOneKeyWithDefaultAndMappingNonexistingKey_ShouldReturnDefaultValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(0).Return(1)
                .Default(-1);

            var result = mapper.Map(1);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Map_ForTwoKeysWithoutDefaultAndMappingKey1_ShouldReturnValue1()
        {
            var mapper = new Mapper<int, int>()
                .Where(1).Return(-1)
                .Where(2).Return(-2);

            var result = mapper.Map(1);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Map_ForTwoKeysWithoutDefaultAndMappingKey2_ShouldReturnValue2()
        {
            var mapper = new Mapper<int, int>()
                .Where(1).Return(-1)
                .Where(2).Return(-2);

            var result = mapper.Map(2);

            Assert.AreEqual(-2, result);
        }

        [Test]
        public void Map_ForTwoKeysWithDefaultAndMappingNonexistingKey_ShouldReturnDefault()
        {
            var mapper = new Mapper<int, int>()
                .Where(1).Return(-1)
                .Where(2).Return(-2)
                .Default(0);

            var result = mapper.Map(3);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Map_ForOneKeyAndValueFactoryAndMappingKey_ShouldReturnFuncResult()
        {
            var mapper = new Mapper<int, int>()
                .Where(0).Return(() => 1);

            var result = mapper.Map(0);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Map_ForOneKeyAndDefaultValueFactoryAndMappingNonexistingKey_ShouldReturnDefaultFuncResult()
        {
            var mapper = new Mapper<int, int>()
                .Where(0).Return(() => 1)
                .Default(() => -1);

            var result = mapper.Map(1);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Map_ForDefaultValueFactoryAndMappingNonexistingKey_ShouldReturnDefaultFuncResult()
        {
            var mapper = new Mapper<int, int>()
                .Default(() => -1);

            var result = mapper.Map(1);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void Map_ForOneMultipleKeyAndMappingExistingFirstKey_ShouldReturnValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(0, 1).Return(() => 2);

            var result = mapper.Map(0);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Map_ForOneMultipleKeyAndMappingExistingSecondKey_ShouldReturnValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(0, 1).Return(() => 2);

            var result = mapper.Map(1);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Map_ForOneMultipleKeyWithTheSameKeys_ShouldThrowArgumentException()
        {
            var mapper = new Mapper<int, int>();

            Assert.Throws<ArgumentException>(() => mapper.Where(1, 1));
        }

        [Test]
        public void Map_ForTwoSameSingleKeys_ShouldThrowArgumentException()
        {
            var mapper = new Mapper<int, int>()
                .Where(1).Return(0);

            Assert.Throws<ArgumentException>(() => mapper.Where(1));
        }

        [Test]
        public void Map_ForKeyFuncAndMappingMatchingKey_ShouldReturnValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(value => value <= 10).Return(1);

            var result = mapper.Map(10);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Map_ForTwoOverlappedKeyFuncsAndMappingKeyMatchingForBoth_ShouldReturnFirstValue()
        {
            var mapper = new Mapper<int, int>()
                .Where(value => value < 10).Return(1)
                .Where(value => value < 100).Return(2);

            var result = mapper.Map(5);

            Assert.AreEqual(1, result);
        }
    }
}