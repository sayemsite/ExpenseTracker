using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Catagory
    {
        [Key]
        public int Id { get; set; }

        //[Remote("ExpenseCategoriesExists", "Catagory", HttpMethod = "POST", ErrorMessage = "Expense Category name already exists in database.")]
        [Required(ErrorMessage = "This field is required and Expense Catagory Name must be unique.")]
        [Column(TypeName = "varchar(30)")]
        [Display(Name = "Expense Catagory")]
        public string Name { get; set; }
        
    }
}
