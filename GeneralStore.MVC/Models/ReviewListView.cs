using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class ReviewListView
    {
        [Display(Name ="Review ID #")]
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Review Title")]
        public string ReviewTitle { get; set; }
        [Display(Name = "Review")]
        public string ReviewMessage { get; set; }
        [Display(Name = "Date Reviewed")]
        public DateTimeOffset DateOfReview { get; set; }



    }
}