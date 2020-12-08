using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Transaction
    {
        [Key]
        [Display(Name ="Transaction ID")]
        public int TransactionID { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        [ForeignKey(nameof(Customer))]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        [Display(Name = "Transaction Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        [Display(Name = "Date/Time of Transaction")]
        public DateTimeOffset TransactionDate { get; set; }


    }
}