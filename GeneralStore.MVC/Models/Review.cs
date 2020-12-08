using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        [Range(0,10, ErrorMessage ="Rating must be between {1} and {2}")]
        public int Rating { get; set; }
        [MaxLength(300,ErrorMessage ="Try a shorter title please")]
        [Display(Name ="Review Title")]
        public string ReviewTitle { get; set; }
        [Display(Name ="Review")]
        [DataType(DataType.MultilineText)]
        [MaxLength(10000, ErrorMessage = "Message too long. Must not exceed 10,000 characters.")]
        public string ReviewMessage { get; set; }
        [Display(Name ="Date of Review UTC")]
        public DateTimeOffset DateOfReviewUTC { get; set; }
        [Display(Name = "Last Date of Editing UTC")]
        public DateTimeOffset? DateOfUpdateUTC { get; set; }
        


    }
}