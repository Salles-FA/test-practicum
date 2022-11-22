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
            var order = "evening,one";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteak()
        {
            var order = "evening,1";
            string expected = "steak";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServe2Potatoes()
        {
            var order = "evening,2,2";
            string expected = "potato(x2)";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteakPotatoWineCake()
        {
            var order = "evening,1,2,3,4";
            string expected = "steak,potato,wine,cake";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanServeSteakPotatox2Cake()
        {
            var order = "evening,1,2,2,4";
            string expected = "steak,potato(x2),cake";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanGenerateErrorWithWrongDish()
        {
            var order = "evening,1,2,3,5";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanGenerateErrorWhenTryingToServerMoreThanOneSteak()
        {
            var order = "evening,1,1,2,3";
            string expected = "error";
            var actual = _sut.TakeOrder(order);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("evening,1", "steak")]
        [TestCase("evening,2,2", "potato(x2)")]
        [TestCase("evening,1,2,3,4", "steak,potato,wine,cake")]
        [TestCase("evening,1,2,2,4", "steak,potato(x2),cake")]
        public void CanEnterAListOfDishTypesWithAtLeastOneSelection(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("evening,3,4,2,1", "steak,potato,wine,cake")]
        [TestCase("evening,1,2,4,2", "steak,potato(x2),cake")]
        public void CanPrintDishNamesInTheFollowingOrderEntreeSideDrinkDessert(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("evening,one", "error")]
        [TestCase("evening,1,2,3,5", "error")]
        [TestCase("evening,1,1,2,3", "error")]
        [TestCase("evening,1,2,3,3", "error")]
        [TestCase("evening,1,2,3,4,4", "error")]
        public void CanPrintErrorIfInvalidSelectionIsEncountered(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("e ve ni ng,  1 ", "steak")]
        [TestCase("evening,  2, 2", "potato(x2)")]
        [TestCase("evening   , 1 ,2 ,3   ,4", "steak,potato,wine,cake")]
        [TestCase("   evening, 1    ,2      ,2,   4", "steak,potato(x2),cake")]
        [TestCase("  evening   ,     3,       4,2     ,1", "steak,potato,wine,cake")]
        [TestCase(" evening,       1, 2, 4,     2       ", "steak,potato(x2),cake")]
        public void CanIgnoreWhitespacesInTheInput(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }


        [TestCase("", "error")]
        [TestCase("evening", "error")]
        [TestCase("evening,", "error")]
        [TestCase("evening,1", "steak")]
        [TestCase("evening,2", "potato")]
        [TestCase("evening,2,2", "potato(x2)")]
        [TestCase("evening,3", "wine")]
        [TestCase("evening,4", "cake")]
        public void CanEnterEachDishTypeAsOptional(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("evening,1,1", "error")]
        [TestCase("evening,2,2", "potato(x2)")]
        [TestCase("evening,3,3", "error")]
        [TestCase("evening,4,4", "error")]
        [TestCase("evening,1,2,4,2,3,2,2,2", "steak,potato(x5),wine,cake")]
        public void CanHaveMultipleOrdersOfPotatoesOnly(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("evening,2,2", "potato(x2)")]
        [TestCase("evening,2,2,2", "potato(x3)")]
        [TestCase("evening,2,2,2,2", "potato(x4)")]
        public void CanOutputMultipleOrdersJustOnceFollowedByQuantityInParentesis(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(" morning, 1, 2, 3 ", "egg,toast,coffee")]
        [TestCase(" Morning,3,3,3 ", "coffee(x3)")]
        [TestCase(" morning ,1,3,2,3 ", "egg,toast,coffee(x2)")]
        [TestCase(" morning, 1, 2, 2 ", "error")]
        [TestCase(" morning, 1, 2, 4 ", "error")]
        [TestCase(" evening,1, 2, 3, 4 ", "steak,potato,wine,cake")]
        [TestCase(" Evening,1, 2, 2, 4 ", "steak,potato(x2),cake")]
        [TestCase(" evening,1, 2, 3, 5 ", "error")]
        [TestCase(" evening,1, 3, 2, 3 ", "error")]
        public void CanRunTheReadmeSampleInputAndOutput(string input, string expected)
        {
            var actual = _sut.TakeOrder(input);
            Assert.AreEqual(expected, actual);
        }
    }
}