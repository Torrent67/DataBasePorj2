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
        private readonly TreatContext _db;

        public TreatController(TreatContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var treats = _db.treats.ToList();
            return View(treats);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(treat treat)
        {
            _db.treats.Add(treat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            var model = _db.treats
            .Include(treat => treat.Genres)
            .ThenInclude(join => join.Genre)
            .FirstOrDefault(treat => treat.treatId == id);
            return View(model);
        }
    }
}