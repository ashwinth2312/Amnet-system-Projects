public class ShoppingCart
{
    private List<CartItem> _cartItems;

    public ShoppingCart()
    {
        _cartItems = new List<CartItem>();
    }

    public void AddItem(int productId, int quantity)
    {
        var existingItem = _cartItems.Find(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            // Fetch product details here (e.g., from a database)
            // assuming you have a ProductService or similar
            var product = ProductService.GetProduct(productId);
            if (product != null)
            {
                _cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name, // Use product details
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }
            else
            {
                // Handle invalid product ID scenario
                throw new Exception("Invalid product ID");
            }
        }
    }

    public List<CartItem> GetCartItems()
    {
        return _cartItems;
    }

    public void UpdateItemQuantity(int productId, int quantity)
    {
        var item = _cartItems.Find(item => item.ProductId == productId);
        if (item != null)
        {
            if (quantity > 0)
            {
                item.Quantity = quantity;
            }
            else
            {
                RemoveItem(productId); // Remove if quantity becomes 0 or negative
            }
        }
        else
        {
            // Handle scenario where item not found in cart
            throw new Exception("Item not found in cart");
        }
    }

    public void RemoveItem(int productId)
    {
        var item = _cartItems.Find(item => item.ProductId == productId);
        if (item != null)
        {
            _cartItems.Remove(item);
        }
        else
        {
            // Handle scenario where item not found in cart (optional)
        }
    }

    public decimal GetCartTotal()
    {
        return _cartItems.Sum(item => item.TotalPrice);
    }
}