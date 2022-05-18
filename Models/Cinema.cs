using CinemaOnline.Data.Base;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CinemaOnline.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Cinema picture is required")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "Cinema name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; }

        public List<Movie> Movies { get; set; }
    }
}