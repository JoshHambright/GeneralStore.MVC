using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        //DB Context
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.OrderBy(trans => trans.TransactionDate).ToList();
            return View(transactionList);
        }

        //Get Transaction/Details/{ID}
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Transaction trans = _db.Transactions.Find(id);
            if (trans == null)
                return HttpNotFound();
            var transDetails = new TransactionDetails
            {
                TransactionID = trans.TransactionID,
                CustomerID = trans.CustomerID,
                CustomerName = trans.Customer.FullName,
                ProductID = trans.ProductID,
                ProductName = trans.Product.Name,
                Quantity = trans.Quantity,
                Price = trans.Product.Price,
                Total = trans.Product.Price * trans.Quantity,
                TransactionDate = trans.TransactionDate
            };
            return View(transDetails);
        }
        //Get Transaction/Create
        public ActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerID", "FullName");
            ViewBag.ProductList = new SelectList(_db.Products, "ProductID", "Name");
            return View();
        }
        //Post: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction trans)
        {
            if (ModelState.IsValid)
            {
                trans.TransactionDate = DateTimeOffset.Now;
                var product = _db.Products.Find(trans.ProductID);
                if (trans.Quantity > product.InventoryCount)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Invalid Quantity. There are only {product.InventoryCount} of {product.Name} let if stock.");
                product.InventoryCount -= trans.Quantity;
                _db.Entry(product).State = EntityState.Modified;
                _db.Transactions.Add(trans);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trans);
        }
        //Get Transaction/Update{id}
        public ActionResult Edit(int? id)
        {
            ViewBag.CustomerList = new SelectList(_db.Customers, "CustomerID", "FullName");
            ViewBag.ProductList = new SelectList(_db.Products, "ProductID", "Name");
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Transaction trans = _db.Transactions.Find(id);

            if (trans == null)
                return HttpNotFound();
            return View(trans);
        }

        //Post Transaction/Update/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction trans)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.Find(trans.ProductID);
                var originalTrans = _db.Transactions
                    .AsNoTracking()
                    .Where(e => e.TransactionID == trans.TransactionID)
                    .FirstOrDefault();
                int origionalInventorybeforeTransaction = product.InventoryCount + originalTrans.Quantity;
                //int newPotentialInventory = product.InventoryCount - trans.Quantity;
                if(trans.Quantity > origionalInventorybeforeTransaction)
                    return new HttpStatusCodeResult(HttpStatusCode
                        .BadRequest, $"Invalid Quantity. Your updated transaction would result in a backorder.");
                product.InventoryCount += originalTrans.Quantity;
                product.InventoryCount -= trans.Quantity;
                trans.TransactionDate = DateTimeOffset.Now;
                _db.Entry(product).State = EntityState.Modified;
                _db.Entry(trans).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trans);
        }

        //Get Transaction/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Transaction trans = _db.Transactions.Find(id);
            if (trans == null)
                return HttpNotFound();
            var transDetails = new TransactionDetails
            {
                TransactionID = trans.TransactionID,
                CustomerID = trans.CustomerID,
                CustomerName = trans.Customer.FullName,
                ProductID = trans.ProductID,
                ProductName = trans.Product.Name,
                Quantity = trans.Quantity,
                Price = trans.Product.Price,
                Total = trans.Product.Price * trans.Quantity,
                TransactionDate = trans.TransactionDate
            };
            return View(transDetails);
        }
        //Post Transaction/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transaction trans = _db.Transactions.Find(id);
            Product product = _db.Products.Find(trans.ProductID);
            product.InventoryCount += trans.Quantity;
            _db.Transactions.Remove(trans);
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}