using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExpenseTracker.Controllers
{
    // Date.Between Extention Method
    public static class DateTimeHelpers
    {
        public static bool Between(this DateTime dateTime, DateTime startDate, DateTime endDate)
        {
            return dateTime >= startDate && dateTime <= endDate;
        }
    }
    
    public class ExpenseViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseViewController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _context.Expenses;

            foreach (var obj in objList)
            {
                obj.CatagoryModel = _context.Catagorys.FirstOrDefault(u => u.Id == obj.CatagoryId);
                
            }
            
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CatagoryDropDown = _context.Catagorys.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
             
            };
            expenseVM.ExpenseList = objList;
           
            return View(expenseVM);

        }

        // GET: ExpenseView
       

        // GET: ExpenseView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.CatagoryModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        public IEnumerable<DateTime> FromAndToDates(DateTime FromDate, DateTime ToDate)
        {
            // check if FromDate is smaller (or the same) as ToDate and code this out or throw error

            // replace dates with actual class / code
            List<DateTime> dates = new List<DateTime>();
            //return dates.Where(d => d >= FromDate && d <= ToDate);

            return dates.Where(x => x.Date.Between(ToDate, ToDate));
        }

        // GET: ExpenseView/Create
        public IActionResult GetDataBetweenTwoDates(ExpenseVM exView, DateTime FromDate, DateTime ToDate)
        {
            if (exView == null)
            {
                return NotFound();
            }

            IEnumerable<Expense> objList = _context.Expenses;
           
            foreach (var obj in objList)
            {
                obj.CatagoryModel = _context.Catagorys.FirstOrDefault(u => u.Id == obj.Id);

            }

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CatagoryDropDown = _context.Catagorys.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            var Expenselist = from exp in _context.Expenses
                              where exp.ExpenseDate == FromDate && exp.ExpenseDate == FromDate && exp.CatagoryId == exView.CatagoryId
                              select exp;

            expenseVM.ExpenseList = objList;

            //return View(Expenselist);
            return View(expenseVM);


        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseName,ExpenseDate,Amount,CatagoryId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatagoryId"] = new SelectList(_context.Catagorys, "Id", "Name", expense.CatagoryId);
            return View(expense);
        }

        // GET: ExpenseView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["CatagoryId"] = new SelectList(_context.Catagorys, "Id", "Name", expense.CatagoryId);
            return View(expense);
        }

        // POST: ExpenseView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseName,ExpenseDate,Amount,CatagoryId")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatagoryId"] = new SelectList(_context.Catagorys, "Id", "Name", expense.CatagoryId);
            return View(expense);
        }

        // GET: ExpenseView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.CatagoryModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: ExpenseView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
