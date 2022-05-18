using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaOnline.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }    
    }
}
