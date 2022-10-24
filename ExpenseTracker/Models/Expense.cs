using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Expense Name")]
        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "varchar(30)")]
        public string ExpenseName { get; set; }

        [Display(Name = "Expense Date")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
        [DateInThePresent]
        public DateTime ExpenseDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage ="Amount must be greater than 0!")]
        [Required(ErrorMessage = "This field is required.")]
        public double Amount { get; set; }

        [DisplayName("Catagory ID")]
        public int CatagoryId { get; set; }

        [Display(Name = "Expense Catagory")]
        [ForeignKey("CatagoryId")]
        public virtual Catagory CatagoryModel { get; set; }
        

        // Entity Framework - Ensure date is in present.
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class DateInThePresentAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                var presentDate = value as DateTime?;
                var memberNames = new List<string>() { context.MemberName };

                if (presentDate != null)
                {
                    if (presentDate.Value.Date > DateTime.UtcNow.Date)
                    {
                        return new ValidationResult("Future Date is not allowed.", memberNames);
                    }
                }

                return ValidationResult.Success;
            }
        }
    

    // Entity Framework - Ensure date is in future.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class DateInTheFutureAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                var futureDate = value as DateTime?;
                var memberNames = new List<string>() { context.MemberName };

                if (futureDate != null)
                {
                    if (futureDate.Value.Date < DateTime.UtcNow.Date)
                    {
                        return new ValidationResult("This must be a date in the future", memberNames);
                    }
                }

                return ValidationResult.Success;
            }
        }
    }

}