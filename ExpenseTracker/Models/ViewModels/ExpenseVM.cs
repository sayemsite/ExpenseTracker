using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models.ViewModels
{
    public class ExpenseVM
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem> CatagoryDropDown { get; set; }
        public IEnumerable<Expense> ExpenseList { get; set; }

        //public int ExpenseId { get; set; }
        //public string ExpenseName { get; set; }
        //public DateTime ExpenseDate { get; set; }
        //public double Amount { get; set; }

        //public SelectList Item { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Selected { get; set; }

        public double TotalAmount { get; set; }

        //public int CatagoryId { get; set; }
        //public string CatagoryName { get; set; }

    }
}
