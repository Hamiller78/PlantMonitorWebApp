using Microsoft.AspNetCore.Mvc;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Controllers
{
    [ApiController]
    [Route("Plants")]
    public class PlantController : ControllerBase
    {
        readonly IPlantInterface _plantRepo;

        public PlantController(IPlantInterface plantRepo) => _plantRepo = plantRepo;

        // GET: Plants/List
        [HttpGet]
        [Route("GetList")]
        public IActionResult List()
        {
            return Ok(_plantRepo.GetAll());
        }

        // GET: Plants/Item/5
        [HttpGet]
        [Route("GetItem")]
        public IActionResult Item(int id)
        {
            Plant? plant = _plantRepo.GetById(id);
            if (plant == null)
            {
                return NotFound();
            }
            return Ok(plant);
        }
/*
        // GET: PlantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
*/
    }
}
