using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class ReviewController : Controller
    {
        //Review DB Context
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            List<Review> reviewList = _db.Reviews.ToList();
            List<Review> orderedList = reviewList
                .OrderBy(review => review.ProductID)
                .ThenBy(review => review.DateOfReviewUTC)
                .ToList();
            return View(orderedList);
        }
        //Get: Review/Create
        public ActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerID", "FullName");
            ViewBag.ProductList = new SelectList(_db.Products, "ProductID", "Name");
            return View();

        }
        //Post: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.DateOfReviewUTC = DateTimeOffset.Now;
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }
        //Get: Review/Details{ID}
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Review review = _db.Reviews.Find(id);
            if (review == null)
                return HttpNotFound();
            return View(review);
        }
        //Get: Review/Update/{id}
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Review review = _db.Reviews.Find(id);
            
            if (review == null)
                return HttpNotFound();
            return View(review);
        }
        //Post: Review/Update{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                review.DateOfUpdateUTC = DateTimeOffset.Now;
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }
        //Get: Review/Delete/{id}
        public ActionResult Delete(int? id)
        {
            ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerID", "FullName");
            ViewBag.ProductList = new SelectList(_db.Products, "ProductID", "Name");
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Review review = _db.Reviews.Find(id);
            if (review == null)
                return HttpNotFound();
            return View(review);
        }

        //Post: Review/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Review review = _db.Reviews.Find(id);
            _db.Reviews.Remove(review);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}