using Contactees.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Contactees.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            PeopleContext ctx = new PeopleContext();
            
            return View(await ctx.GetPeopleAsync());
        }

        public async Task<ActionResult> PersonEditForm(Guid id)
        {
            PeopleContext ctx = new PeopleContext();
            var person = await ctx.GetPersonAsync(id.ToString());
            return PartialView("_EditPersonView", person);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> PostEditPerson(Person model)
        {
            if (!ModelState.IsValid)
                return PartialView("_EditPersonView", model);
            bool verified = await VarifyNationalId(model);
            if (!verified)
            {
                ModelState.AddModelError("IdNumber", "National identity number is already in use");
                return PartialView("_EditPersonView", model);
            }
            PeopleContext ctx = new PeopleContext();
            await ctx.UpdatePersonAsync(model);
            return RedirectToAction("Index");
        }

        private async Task<bool> VarifyNationalId(Person model)
        {
            PeopleContext ctx = new PeopleContext();
            bool existsWithSameNationalId = await ctx.CheckNationalIdInUse(model.Id, model.IdNumber);
            if (existsWithSameNationalId)
                return false;
            return true;
        }

        public ActionResult PersonCreateForm()
        {
            Person person = new Person
            {
                Id = Guid.NewGuid()
            };
            return PartialView("_CreatePersonView", person);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> PostNewPerson(Person model)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreatePersonView", model);
            bool verified = await VarifyNationalId(model);
            if (!verified)
            {
                ModelState.AddModelError("IdNumber", "National identity number is already in use");
                return PartialView("_CreatePersonView", model);
            }
            PeopleContext ctx = new PeopleContext();
            await ctx.CreatePersonAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> PersonDeleteForm(Guid id)
        {
            PeopleContext ctx = new PeopleContext();
            var person = await ctx.GetPersonAsync(id.ToString());
            return PartialView("_DeletePersonView", person);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> DeletePerson(Person model)
        {
            PeopleContext ctx = new PeopleContext();
             await ctx.DeletePersonAsync(model.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
