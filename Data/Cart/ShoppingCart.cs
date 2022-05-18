using CinemaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaOnline.Data.Cart
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        private ApplicationDbContext _context { get; set; }
        public ShoppingCart(ApplicationDbContext context)
        {
             _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = _context.ShoppingCartItems.
                Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }
        public double GetShoppingCartSummary()
        {
            return _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
        }
        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(m => m.Movie.Id == movie.Id &&
                        m.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    Amount = 1,
                    Movie = movie,
                    ShoppingCartId = ShoppingCartId
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(m => m.Movie.Id == movie.Id &&
                        m.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
                _context.SaveChanges();
            }
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = _context.ShoppingCartItems.Where(m => m.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
