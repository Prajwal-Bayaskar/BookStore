using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface ICartService
    {
        void AddToCart(Books book, int quantity);
        List<CartItem> GetCartItems();
        void RemoveFromCart(int bookId);
        decimal GetTotal();
    }
}
