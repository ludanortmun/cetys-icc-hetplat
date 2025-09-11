namespace DiscountCodes.Models
{
    public class ShoppingCart
    {
        private List<Product> items = new List<Product>();
        public IReadOnlyList<Product> Items => items.AsReadOnly();

        public void AddItem(Product product)
        {
            items.Add(product);
        }

        /// <summary>
        /// Returns the total price of all items in the cart, after applying any discounts.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal()
        {
            return items.Sum(item => item.Price);
        }

        /// <summary>
        /// Applies a discount code to the shopping cart.
        /// If the code is invalid or empty, no discount is applied.
        /// Only one discount code can be applied at a time, applying a new code replaces the previous one.
        /// </summary>
        /// <param name="code"></param>
        public void ApplyDiscount(string code)
        {
            // TODO: Implement discount codes
        }
    }
}
