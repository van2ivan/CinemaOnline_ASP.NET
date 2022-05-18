using CinemaOnline.Data.Services;
using Microsoft.AspNetCore.Mvc;
using CinemaOnline.Models;
using Microsoft.AspNetCore.Authorization;
using CinemaOnline.Data.Static;

namespace CinemaOnline.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var _actor = await _service.GetByIdAsync(id);
            if (_actor == null) return View("NotFound");
            return View(_actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (string.IsNullOrEmpty(cinema.Name) || string.IsNullOrEmpty(cinema.Picture) || string.IsNullOrEmpty(cinema.Description))
            {
                return View(cinema);
            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (string.IsNullOrEmpty(cinema.Name) || string.IsNullOrEmpty(cinema.Picture) || string.IsNullOrEmpty(cinema.Description)) 
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
