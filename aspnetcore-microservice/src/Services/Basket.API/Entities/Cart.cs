namespace Basket.API.Entities;

public class Cart
{
    public string UserName { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
    public Cart() { }
    public Cart(string userName)
    {
        UserName = userName;
    }
}
