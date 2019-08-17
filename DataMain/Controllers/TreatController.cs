using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using DataBase.Models;


namespace DataBase.Controllers
{

    class TreatController : Controller
    {
        private readonly DataBaseContext _db;

        public TreatController(DataBaseContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var treats = _db.Treats.ToList();
            return View(treats);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Treat treat)
        {
            _db.Treats.Add(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            var model = _db.Treats
            .Include(treat => treat.Genre)
            .ThenInclude(join => join.Genre)
            .FirstOrDefault(treat => treat.treatId == id);
            return View(model);
        }
    }
}