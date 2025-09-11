using DiscountCodes.Models;

namespace DiscountCodes.Tests
{
    public class DiscountCodesTests
    {
        private static Product _testProduct1 = new("Brand1", "Product1", 10.99m);
        private static Product _testProduct2 = new("Brand2", "Product2", 20.50m);
        private static Product _testProduct3 = new("Brand2", "Product3", 30.00m);

        private static (string, Product[], decimal)[] _testCases =
        [
            ("BOGOFREE", [_testProduct1, _testProduct1 ], _testProduct1.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct2, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("BOGOFREE", [_testProduct1, _testProduct1, _testProduct1], _testProduct1.Price * 2),
            ("BRAND2DISCOUNT", [_testProduct2, _testProduct3], (_testProduct2.Price + _testProduct3.Price) * 0.9m),
            ("BRAND2DISCOUNT", [_testProduct1, _testProduct2, _testProduct3], (_testProduct1.Price + (_testProduct2.Price + _testProduct3.Price) * 0.9m)),
            ("10PERCENTOFF", [_testProduct1, _testProduct2], (_testProduct1.Price + _testProduct2.Price) * 0.9m),
            ("10PERCENTOFF", [_testProduct1, _testProduct2, _testProduct3], (_testProduct1.Price + _testProduct2.Price + _testProduct3.Price) * 0.9m),
            ("BRAND1MANIA", [_testProduct1, _testProduct1, _testProduct2], (_testProduct1.Price * 2 * 0.5m) + _testProduct2.Price),
            ("BRAND1MANIA", [_testProduct1, _testProduct1, _testProduct1], (_testProduct1.Price * 3 * 0.5m)),
            ("5USDOFF", [_testProduct1, _testProduct2], Math.Max(0, (_testProduct1.Price + _testProduct2.Price - 5.00m))),
            ("5USDOFF", [_testProduct1, _testProduct2, _testProduct3], Math.Max(0, (_testProduct1.Price + _testProduct2.Price + _testProduct3.Price - 5.00m))),
            ("", [_testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("", [_testProduct1, _testProduct2, _testProduct3], _testProduct1.Price + _testProduct2.Price + _testProduct3.Price),
            ("INVALIDCODE", [_testProduct1, _testProduct2], _testProduct1.Price + _testProduct2.Price),
            ("INVALIDCODE", [_testProduct1, _testProduct2, _testProduct3], _testProduct1.Price + _testProduct2.Price + _testProduct3.Price),
        ];


        private ShoppingCart _cart;

        [SetUp]
        public void Setup()
        {
            _cart = new ShoppingCart();
        }

        [TestCaseSource(nameof(_testCases))]
        public void ApplyDiscounts_WhenCalled_ReturnsExpectedTotal((string, Product[], decimal) testCase)
        {
            var (code, products, expectedTotal) = testCase;

            // Arrange
            foreach (var product in products)
            {
                _cart.AddItem(product);
            }

            // Act
            _cart.ApplyDiscount(code);
            var total = _cart.GetTotal();

            // Assert
            Assert.That(total, Is.EqualTo(expectedTotal), code);
        }

    }
}
