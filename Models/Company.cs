using CinemaOnline.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace CinemaOnline.Models
{
    public class Company : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Picture is required")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Minimum name length is 4")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Minimum description length is 20")]
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
