using CinemaOnline.Data;
using CinemaOnline.Data.Services;
using CinemaOnline.Data.Static;
using CinemaOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaOnline.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var _actor = await _service.GetAllAsync();
            return View(_actor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (string.IsNullOrEmpty(actor.FullName) || string.IsNullOrEmpty(actor.ProfilePicture) || string.IsNullOrEmpty(actor.Biography))
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var _actor = await _service.GetByIdAsync(id);
            if (_actor == null) return View("NotFound");
            return View(_actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (string.IsNullOrEmpty(actor.FullName) || string.IsNullOrEmpty(actor.ProfilePicture) || string.IsNullOrEmpty(actor.Biography))
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _actor = await _service.GetByIdAsync(id);
            if (_actor == null) return View("NotFound");

            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View(result);
        }
    }
}

