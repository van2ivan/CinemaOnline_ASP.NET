using CinemaOnline.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaOnline.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Profile picture is required")]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Minimum name length is 4")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Minimum biography length is 20")]
        public string Biography { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}