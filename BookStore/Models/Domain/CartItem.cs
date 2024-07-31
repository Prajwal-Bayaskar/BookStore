namespace BookStore.Models.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
