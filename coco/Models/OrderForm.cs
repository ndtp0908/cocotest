namespace coco.Models;
public class OrderForm
{
    public string? UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Discount { get; set; }=0;
    public decimal Total { get; set; }
    public List<CartItem> Cart { get; set; } = new List<CartItem>();
}

public class CartItem
{
    public string ItemId { get; set; } = string.Empty;
    public int ItemAmount { get; set; }
    public decimal ItemPrice { get; set; }
}
