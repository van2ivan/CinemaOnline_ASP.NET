﻿namespace CinemaOnline.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public virtual Movie Movie { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
