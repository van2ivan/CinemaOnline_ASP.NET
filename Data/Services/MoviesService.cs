using CinemaOnline.Data.Base;
using CinemaOnline.Data.ViewModels;
using CinemaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaOnline.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService 
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var result = await _context.Movies.
                Include(c => c.Cinema).
                Include(c => c.Company).
                Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).
                FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<NewMovieDropdownsVM> GetMovieDropdowns()
        {
            var dropdowns = new NewMovieDropdownsVM()
            {
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Companies = await _context.Companies.OrderBy(n => n.Name).ToListAsync()
            };
            return dropdowns;
        }
        public async Task AddNewMovieAsync(NewMovieVM movie)
        {
            var _movie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                Picture = movie.Picture,
                Price = movie.Price,
                Category = movie.Category,
                CinemaId = movie.CinemaId,
                CompanyId = movie.CompanyId,
            };
            await _context.Movies.AddAsync(_movie);
            await _context.SaveChangesAsync();
            foreach(var id in movie.ActorIds)
            {
                var actor_movie = new Actor_Movie()
                {
                    MovieId = _movie.Id,
                    ActorId = id
                };
                await _context.Actors_Movies.AddAsync(actor_movie);
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateNewMovieAsync(NewMovieVM movie)
        {
            var _movie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if(_movie != null)
            {
                _movie.Name = movie.Name;
                _movie.Description = movie.Description;
                _movie.StartDate = movie.StartDate;
                _movie.EndDate = movie.EndDate;
                _movie.Picture = movie.Picture;
                _movie.Price = movie.Price;
                _movie.Category = movie.Category;
                _movie.CinemaId = movie.CinemaId;
                _movie.CompanyId = movie.CompanyId;
                await _context.SaveChangesAsync();
            }

            var _actor_movie = _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToList();
            _context.Actors_Movies.RemoveRange(_actor_movie);
            await _context.SaveChangesAsync();

            foreach (var id in movie.ActorIds)
            {
                var actor_movie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = id
                };
                await _context.Actors_Movies.AddAsync(actor_movie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
