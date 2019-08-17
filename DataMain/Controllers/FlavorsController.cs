using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using DataBase.Models;

namespace DataBase.Controllers
{
    [Authorize]
    public class FlavorsController : Controller
    {
        private readonly DataBaseContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlavorsController(UserManager<ApplicationUser> userManager, DataBaseContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Flavors.Where(x => x.User.Id == currentUser.Id).ToList());

        }

        public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(Flavors flavor, int TreatId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            flavor.User = currentUser;
            _db.Flavors.Add(flavor);
            if (TreatId != 0)
            {
                _db.FlavorTreats.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorsId });

            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisFlavor = _db.Flavors
                .Include(flavor => flavor.Treats)
                .ThenInclude(join => join.Treat)
                .FirstOrDefault(flavor => flavor.FlavorsId == id);
            return View(thisFlavor);
        }

        public ActionResult Edit(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorsId == id);
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult Edit(Flavors flavor, int TreatId)
        {
            if (TreatId != 0)
            {
                _db.FlavorTreats.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorsId });
            }
            _db.Entry(flavor).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorsId == id);
            return View(thisFlavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorsId == id);
            _db.Flavors.Remove(thisFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int joinId)
        {
            var joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreats.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}