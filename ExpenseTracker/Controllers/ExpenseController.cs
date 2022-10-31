using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;

            foreach(var obj in objList)
            {
                obj.CatagoryModel = _db.Catagorys.FirstOrDefault(u => u.Id == obj.CatagoryId);
            }

            return View(objList);
            
        }

        // GET-Create
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> CatagoryDropDown = _db.Catagory.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.CatagoryDropDown = CatagoryDropDown;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                //Expense = new Expense(),
                CatagoryDropDown = _db.Catagorys.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                //obj.ExpenseTypeId = 1;
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }


        // GET Delete
        public IActionResult Delete(int? id)
        {
           
            if (id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
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
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
           
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET Update
        public IActionResult Update(int? id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CatagoryDropDown = _db.Catagorys.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense = _db.Expenses.Find(id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
    }
}
