using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CatagoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Catagory> objList = _db.Catagorys;
            return View(objList);
            
        }

        // GET-Create
        public IActionResult Create()
        {
            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catagory obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Catagorys.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch(Exception e)
            {

                TempData["CatagoryActionResult"] = e.InnerException.Message.ToString();
                TempData["newCatagoryName"] = obj.Name.ToString();
                return View();
            }
        }

        

        // GET Delete
        public IActionResult Delete(int? id)
        {
           
            if (id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Catagorys.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Catagorys.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
           
            _db.Catagorys.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET Update
        public IActionResult Update(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Catagorys.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Catagory obj)
        {
            if (ModelState.IsValid)
            {
                _db.Catagorys.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
