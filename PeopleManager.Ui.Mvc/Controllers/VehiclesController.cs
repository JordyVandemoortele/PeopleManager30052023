using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Models;
using PeopleManager.Services;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleService _VehicleService;
        private readonly PersonService _PersonService;
        public VehiclesController(VehicleService VehicleService, PersonService PersonService)
        {
            _VehicleService = VehicleService;
            _PersonService = PersonService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _VehicleService.Find();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Create", vehicle);
            }
            _VehicleService.Create(vehicle);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicle = _VehicleService.Get(id);
            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }
            return CreateEditView("Edit", vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [FromForm]Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Edit", vehicle);
            }
            _VehicleService.Update(id, vehicle);
            return RedirectToAction("Index");
        }
        
        private IActionResult CreateEditView([AspMvcView]string viewName, Vehicle? vehicle = null)
        {
            var people = _PersonService.Find();
            ViewBag.People = people;
            return View(viewName, vehicle);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vehicle = _VehicleService.Get(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpPost("Vehicles/Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var vehicle = _dbContext.Vehicles.Find(id);

            //if (vehicle is null)
            //{
            //    return RedirectToAction("Index");
            //}
            _VehicleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
