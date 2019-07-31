using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FDMC.App.Models;
using FDMC.Data;
using FDMC.App.Models.ViewModels;

namespace FDMC.App.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(CatContext context)
        {
            this.Context = context;
        }

        public CatContext Context { get; private set; }

        public IActionResult Index()
        {
            var cats = this.Context.Cats
                .Select(cat => new CatConciseViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Name
                })
                .ToList();

            return View(cats);
        }
    }
}
