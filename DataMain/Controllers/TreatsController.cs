using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using DataBase.Models;


namespace DataBase.Controllers
{
    
    public class TreatsController : Controller
    {
        private readonly DataBaseContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TreatsController(DataBaseContext db, UserManager<ApplicationUser> usermanager)
        {
            _db = db;
            _userManager = usermanager;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Treats.Where(x => x.User.Id == currentUser.Id).ToList());

        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var model = _db.Treats
            .Include(treat => treat.Flavors)
            .ThenInclude(join => join.Flavor)
            .FirstOrDefault(treat => treat.TreatId == id);
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Description");
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Treat treat)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            treat.User = currentUser;
            _db.Treats.Add(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Description");
            var thisTreat = _db.Treats.FirstOrDefault(categories => categories.TreatId == id);
            return View(thisTreat);
        }

        public ActionResult Delete(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
            return View(thisTreat);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
            _db.Treats.Remove(thisTreat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult AddFlavor(Treat treat, int FlavorId)
        {
            if (FlavorId != 0)
            {
                _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
            }
            _db.Entry(treat).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = treat.TreatId });
        }

        [HttpPost]
        public ActionResult DeleteFlavor(int joinId, int treatId)
        {
            var joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreats.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = treatId });
        }
    }
}