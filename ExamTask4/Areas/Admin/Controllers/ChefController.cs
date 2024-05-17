using ExamTask4.Business.Exceptions;
using ExamTask4.Business.Services.Abstracts;
using ExamTask4.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly IChefService _chefService;

        public ChefController(IChefService chefService)
        {
            _chefService = chefService;
        }

        public IActionResult Index()
        {
            var chefs = _chefService.GetAllChefs();
            return View(chefs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Chef chef)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _chefService.AddChef(chef);
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("ImageFile",ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var existChef = _chefService.GetChef(x=>x.Id == id);
            if(existChef == null) return NotFound();
            return View(existChef);
        }
        [HttpPost]
        public IActionResult Update(Chef chef)
        {
            if(!ModelState.IsValid) 
                return View();
            try
            {
                _chefService.UpdateChef(chef.Id, chef);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ExamTask4.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var existChef = _chefService.GetChef(x=>x.Id == id);
            if (existChef == null) return NotFound();
            return View(existChef);
        }
        public IActionResult DeletePost(int id)
        {
            try
            {
                _chefService.DeleteChef(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (ExamTask4.Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
