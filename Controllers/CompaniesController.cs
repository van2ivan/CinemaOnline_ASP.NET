using CinemaOnline.Data.Static;
using CinemaOnline.Models;
using CompanyOnline.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaOnline.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _service;
        public CompaniesController(ICompaniesService service)
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
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Picture) || string.IsNullOrEmpty(company.Description))
            {
                return View(company);
            }
            await _service.UpdateAsync(id, company);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Picture) || string.IsNullOrEmpty(company.Description))
            {
                return View(company);
            }
            await _service.AddAsync(company);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
