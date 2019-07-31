using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models.BindingModels;
using Panda.Data;
using Panda.Domain;

namespace Panda.App.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PandaDbContext context;

        public PackagesController(PandaDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PackageCreateBindingModel model)
        {
            var package = new Package
            {
                Description = model.Description,
                Recipient = this.context.Users.SingleOrDefault(user => user.UserName == model.Recipient),
                ShippingAddress = model.ShippingAddress,
                Wight = model.Weight,
                Status = this.context.PackageStatus.SingleOrDefault(status => status.Name == "Pending"),
            };

            this.context.Packages.Add(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Pending");
        }

        [HttpGet("/Packages/Ship/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            var package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatus.SingleOrDefault(status => status.Name == "Shipped");
            package.EstimatedDeliveryDate = DateTime.Now.AddDays(new Random().Next(20, 40));
            this.context.Update(package);
            this.context.SaveChanges();
            
            return this.Redirect("/Packages/Shipped");
        }

        [HttpGet("/Packages/Deliver/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            var package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatus.SingleOrDefault(status => status.Name == "Delivered");
            this.context.Update(package);
            this.context.SaveChanges();

            return this.Redirect("/Packages/Delivered");
        }

        [HttpGet("/Packages/Acquire/{id}")]
        [Authorize]
        public IActionResult Acquire(string id)
        {
            var package = this.context.Packages.Find(id);
            package.Status = this.context.PackageStatus.SingleOrDefault(status => status.Name == "Acquired");
            this.context.Update(package);

            Receipt receipt = new Receipt
            {
                Fee = (decimal)(2.67 * package.Wight),
                IssuedOn = DateTime.Now,
                Package = package,
                Recipient = context.Users.SingleOrDefault(user => user.UserName == this.User.Identity.Name),
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();

            return this.Redirect("/Home/Index");
        }

        [HttpGet("/Packages/Details/{id}")]
        [Authorize]
        public IActionResult Details(string id)
        {
            Package package = this.context.Packages
                .Where(p => p.Id == id)
                .Include(p => p.Recipient)
                .Include(p => p.Status)
                .SingleOrDefault();
        
            PackageDetailsViewModel viewModel = new PackageDetailsViewModel
            {
                Description = package.Description,
                Recipient = package.Recipient.UserName,
                ShippingAddress = package.ShippingAddress,
                Weight = package.Wight,
                Status = package.Status.Name
            };

            if (package.Status.Name == "Pending")
            {
                viewModel.EstimatedDeliveryDate = "N/A";
            }
            else if (package.Status.Name == "Shipped")
            {
                viewModel.EstimatedDeliveryDate = package.EstimatedDeliveryDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                viewModel.EstimatedDeliveryDate = "Delivered";
            }

            return this.View(viewModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Pending")
                .ToList().Select(package => 
            {
                return new PackagePendingViewModel
                {
                    Id = package.Id,
                    Description = package.Description,
                    Weight = package.Wight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.UserName
                };
            }).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Shipped")
                .ToList().Select(package =>
                {
                    return new PackageShippedViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Wight,
                        EstimatedDeliveryDate = package.EstimatedDeliveryDate?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Recipient = package.Recipient.UserName
                    };
                }).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            return this.View(context.Packages
                .Include(package => package.Recipient)
                .Where(package => package.Status.Name == "Delivered" || package.Status.Name == "Acquired")
                .ToList().Select(package =>
                {
                    return new PackageDeliveredViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Weight = package.Wight,
                        ShippingAddress = package.ShippingAddress,
                        Recipient = package.Recipient.UserName
                    };
                }).ToList());
        }
    }
}