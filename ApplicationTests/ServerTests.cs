using Application;
using NUnit.Framework;

namespace ApplicationTests
{
    [TestFixture]
    public class ServerTests
    {
        private Server _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Server(new DishManager());
        }

        [TearDown]
        public void Teardown()
        {

        }

        [Test]
        public void ErrorGetsReturnedWithBadInput()
        {
            var order = "one";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteak()
        {
            var order = "1";
            string expected = "steak";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServe2Potatoes()
        {
            var order = "2,2";
            string expected = "potato(x2)";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteakPotatoWineCake()
        {
            var order = "1,2,3,4";
            string expected = "steak,potato,wine,cake";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteakPotatox2Cake()
        {
            var order = "1,2,2,4";
            string expected = "steak,potato(x2),cake";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanGenerateErrorWithWrongDish()
        {
            var order = "1,2,3,5";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanGenerateErrorWhenTryingToServerMoreThanOneSteak()
        {
            var order = "1,1,2,3";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1", "steak")]
        [TestCase("2,2", "potato(x2)")]
        [TestCase("1,2,3,4", "steak,potato,wine,cake")]
        [TestCase("1,2,2,4", "steak,potato(x2),cake")]
        public void CanEnterAListOfDishTypesWithAtLeastOneSelection(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("3,4,2,1", "steak,potato,wine,cake")]
        [TestCase("1,2,4,2", "steak,potato(x2),cake")]
        public void CanPrintDishNamesInTheFollowingOrderEntreeSideDrinkDessert(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("one", "error")]
        [TestCase("1,2,3,5", "error")]
        [TestCase("1,1,2,3", "error")]
        [TestCase("1,2,3,3", "error")]
        [TestCase("1,2,3,4,4", "error")]
        public void CanPrintErrorIfInvalidSelectionIsEncountered(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1 ", "steak")]
        [TestCase(" 2, 2", "potato(x2)")]
        [TestCase(" 1 ,2 ,3   ,4", "steak,potato,wine,cake")]
        [TestCase("1    ,2      ,2,   4", "steak,potato(x2),cake")]
        [TestCase("     3,       4,2     ,1", "steak,potato,wine,cake")]
        [TestCase("        1, 2, 4,     2       ", "steak,potato(x2),cake")]
        public void CanIgnoreWhitespacesInTheInput(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }


        [TestCase("", "error")]
        [TestCase("1", "steak")]
        [TestCase("2", "potato")]
        [TestCase("2,2", "potato(x2)")]
        [TestCase("3", "wine")]
        [TestCase("4", "cake")]
        public void CanEnterEachDishTypeAsOptional(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,1", "error")]
        [TestCase("2,2", "potato(x2)")]
        [TestCase("3,3", "error")]
        [TestCase("4,4", "error")]
        [TestCase("1,2,4,2,3,2,2,2", "steak,potato(x5),wine,cake")]
        public void CanHaveMultipleOrdersOfPotatoesOnly(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("2,2", "potato(x2)")]
        [TestCase("2,2,2", "potato(x3)")]
        [TestCase("2,2,2,2", "potato(x4)")]
        public void CanOutputMultipleOrdersJustOnceFollowedByQuantityInParentesis(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }
    }
}