namespace BulkyWeb.Models
{
    public class CartItem
{
    public int Id { get; set; } // Primary key
    public int CartId { get; set; } // Foreign key to Cart
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

}
