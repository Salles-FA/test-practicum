using System.Collections.Generic;
using System.Linq;
using Application;
using NUnit.Framework;


namespace ApplicationTests
{
    [TestFixture]
    public class DishManagerTests
    {
        private DishManager _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DishManager();
        }

        [Test]
        public void EmptyListReturnsEmptyList()
        {
            var order = new Order();
            var actual = _sut.GetDishes(order);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public void ListWith1ReturnsOneSteak()
        {
            var order = new Order
            {
                DishesId = new List<int>
                {
                    1
                }
            };

            var actual = _sut.GetDishes(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("steak", actual.First().Name);
            Assert.AreEqual(1, actual.First().Count);
        }

        [TestCase(1, "steak")]
        [TestCase(2, "potato")]
        [TestCase(3, "wine")]
        [TestCase(4, "cake")]
        public void ListWithOneReturnsOneDish(int input, string expected)
        {
            var order = new Order
            {
                DishesId = new List<int>
                {
                    input
                }
            };

            var actual = _sut.GetDishes(order);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected, actual.First().Name);
            Assert.AreEqual(1, actual.First().Count);
        }
    }
}
