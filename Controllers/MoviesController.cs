using CinemaOnline.Data.Services;
using CinemaOnline.Data.Static;
using CinemaOnline.Data.ViewModels;
using CinemaOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaOnline.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync(n => n.Cinema);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            var data = await _service.GetMovieDropdowns();
            ViewBag.CompanyId = new SelectList(data.Companies, "Id", "Name");
            ViewBag.ActorIds = new SelectList(data.Actors, "Id", "FullName");
            ViewBag.CinemaId = new SelectList(data.Cinemas, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdowns = await _service.GetMovieDropdowns();

                ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdowns.Companies, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");

                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetMovieByIdAsync(id);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");
            var result = new NewMovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                StartDate = movie.StartDate,
                Picture = movie.Picture,
                EndDate = movie.EndDate,
                Price = movie.Price,
                Category = movie.Category,
                CinemaId = movie.CinemaId,
                CompanyId = movie.CompanyId,
                ActorIds = movie.Actors_Movies.Select(n => n.ActorId).ToList()
            };
            var dropdowns = await _service.GetMovieDropdowns();

            ViewBag.CinemaId = new SelectList(dropdowns.Cinemas, "Id", "Name");
            ViewBag.CompanyId = new SelectList(dropdowns.Companies, "Id", "Name");
            ViewBag.ActorIds = new SelectList(dropdowns.Actors, "Id", "FullName");

            return View(result);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View(nameof(NotFound));
            if (!ModelState.IsValid)
            {
                var dropdowns = await _service.GetMovieDropdowns();

                ViewBag.CinemaId = new SelectList(dropdowns.Cinemas, "Id", "Name");
                ViewBag.CompanyId = new SelectList(dropdowns.Companies, "Id", "Name");
                ViewBag.ActorIds = new SelectList(dropdowns.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchString)
        {
            var movies = await _service.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var result = movies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View(nameof(Index), result);
            }
            return View(nameof(Index), movies);
        }
    }
}
