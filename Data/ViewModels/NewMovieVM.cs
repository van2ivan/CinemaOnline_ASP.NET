using CinemaOnline.Data.Enums;
using CinemaOnline.Models;
using System.ComponentModel.DataAnnotations;

namespace CinemaOnline.Data.ViewModels
{
    public class NewMovieVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum name length is 2")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Picture is required")]
        public string Picture { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "Minimum description length is 2")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; }

        [Required(ErrorMessage = "Movie company is required")]
        public int CompanyId { get; set; }

    }
}
