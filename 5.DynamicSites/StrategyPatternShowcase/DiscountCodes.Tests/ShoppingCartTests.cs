using DiscountCodes.Models;

namespace DiscountCodes.Tests
{
    public class ShoppingCartTests
    {
        private ShoppingCart _cart;
        private Product _testProduct1;
        private Product _testProduct2;

        [SetUp]
        public void Setup()
        {
            _cart = new ShoppingCart();
            _testProduct1 = new Product("Brand1", "Product1", 10.99m);
            _testProduct2 = new Product("Brand2", "Product2", 20.50m);
        }

        [Test]
        public void AddItem_WhenCalled_AddsProductToCart()
        {
            // Act
            _cart.AddItem(_testProduct1);

            // Assert
            Assert.That(_cart.Items, Has.Count.EqualTo(1));
            Assert.That(_cart.Items[0], Is.EqualTo(_testProduct1));
        }

        [Test]
        public void GetTotal_WithMultipleItems_ReturnsSumOfPrices()
        {
            // Arrange
            _cart.AddItem(_testProduct1);
            _cart.AddItem(_testProduct2);

            // Act
            var total = _cart.GetTotal();

            // Assert
            Assert.That(total, Is.EqualTo(31.49m));
        }

        [Test]
        public void GetTotal_WithEmptyCart_ReturnsZero()
        {
            // Act
            var total = _cart.GetTotal();

            // Assert
            Assert.That(total, Is.EqualTo(0m));
        }

        [Test]
        public void Items_IsReadOnly_CannotBeModifiedDirectly()
        {
            // Arrange
            _cart.AddItem(_testProduct1);

            // Assert
            Assert.That(_cart.Items, Is.TypeOf<System.Collections.ObjectModel.ReadOnlyCollection<Product>>());
        }
    }
}
