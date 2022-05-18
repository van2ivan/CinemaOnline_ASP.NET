using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaOnline.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
