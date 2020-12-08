using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class TransactionDetails
    {
        [Key]
        [Display(Name = "Transaction ID")]
        public int TransactionID { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Transaction Quantity")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        [Display(Name = "Date/Time of Transaction")]
        public DateTimeOffset TransactionDate { get; set; }
    }
}