using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Repositories.Abstract;
using BookStore.Services;
using BookStore.Repositories.Implementation;
using YourProjectNamespace.Services;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBookService _bookRepository;
        private readonly IPaymentService _paymentService;

        public CartController(ICartService cartService, IBookService bookRepository, IPaymentService paymentService)
        {
            _cartService = cartService;
            _bookRepository = bookRepository;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            var book = _bookRepository.GetById(bookId);
            if (book != null)
            {
                _cartService.AddToCart(book, quantity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(PaymentInfo paymentInfo)
        {
            var amount = _cartService.GetTotal();
            if (_paymentService.ProcessPayment(paymentInfo, amount))
            {
                // Clear the cart after successful payment
                foreach (var item in _cartService.GetCartItems().ToList())
                {
                    _cartService.RemoveFromCart(item.Book.Id);
                }
                return RedirectToAction("PaymentSuccess");
            }
            return View("Index", _cartService.GetCartItems());
        }

        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PayNow()
        {
            return View();
        }
    }
}

