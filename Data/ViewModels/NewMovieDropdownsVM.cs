using CinemaOnline.Models;
namespace CinemaOnline.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Cinemas = new List<Cinema>();
            Companies = new List<Company>();
            Actors = new List<Actor>();
        }
        public List<Cinema> Cinemas;
        public List<Company> Companies;
        public List<Actor> Actors;
    }
}
