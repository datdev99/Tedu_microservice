namespace Basket.API.Entities
{
    public class CartItem
    {
        public CartItem() { }
        public CartItem(int id) { }
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
