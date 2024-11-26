public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } // Add this for better user experience
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get { return UnitPrice * Quantity; } } // Calculate total price on demand
}