using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cartItems = new List<CartItem>();

        public void AddToCart(Books book, int quantity)
        {
            var cartItem = _cartItems.SingleOrDefault(c => c.Id == book.Id);
            if (cartItem == null)
            {
                _cartItems.Add(new CartItem { Book = book, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        public void RemoveFromCart(int bookId)
        {
            var cartItem = _cartItems.SingleOrDefault(c => c.Id == bookId);
            if (cartItem != null)
            {
                _cartItems.Remove(cartItem);
            }
        }

        public decimal GetTotal()
        {
            return _cartItems.Sum(c => c.Book.Price * c.Quantity);
        }

       
    }
}

