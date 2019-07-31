using FDMC.App.Models.BindingModels;
using FDMC.App.Models.ViewModels;
using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.App.Controllers
{
    public class CatsController: Controller
    {
        public CatsController(CatContext context)
        {
            this.Context = context;
        }

        public CatContext Context { get; private set; }

        public IActionResult Details(int id)
        {
            var cat = this.Context.Cats.Find(id);
            if (cat == null)
            {
                return NotFound();
            }

            var catModel = new CatDetailsViewModel()
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };

            return View(catModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CatCreatingBindingModel model)
        {
            var cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };

            this.Context.Cats.Add(cat);
            this.Context.SaveChanges();

            return RedirectToAction("Details", new { id = cat.Id });
        }
    }
}
