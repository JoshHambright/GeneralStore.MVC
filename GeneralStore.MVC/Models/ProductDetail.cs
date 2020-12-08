using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class ProductDetail
    {
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Inventory Count")]

        public int InventoryCount { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Is the Item Perishable")]
        public bool IsFood { get; set; }
        public virtual List<ReviewListView> Reviews { get; set; } = new List<ReviewListView>();
        [Display(Name = "Avg Customer Review")]
        public virtual double AvgReview { get; set; }
    }
}