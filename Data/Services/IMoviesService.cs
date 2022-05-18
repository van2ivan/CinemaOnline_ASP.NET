using CinemaOnline.Data.Base;
using CinemaOnline.Data.ViewModels;
using CinemaOnline.Models;

namespace CinemaOnline.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetMovieDropdowns();
        Task AddNewMovieAsync(NewMovieVM movie);
        Task UpdateNewMovieAsync(NewMovieVM movie);
    }
}

