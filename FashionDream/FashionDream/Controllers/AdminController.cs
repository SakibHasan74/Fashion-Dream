using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionDream.Models;
using System.IO;
namespace FashionDream.Controllers
{
    public class AdminController : Controller
    {

        FashionDream1Entities12 FashionDatabase = new FashionDream1Entities12();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Display(UserAccount user)
        {
            var obj = FashionDatabase.UserAccounts.ToList();
            return View(obj);
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = FashionDatabase.UserAccounts.Find(id);
           
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(UserAccount user)
        {
            FashionDatabase.Entry(user).State = System.Data.Entity.EntityState.Modified;
            FashionDatabase.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = FashionDatabase.UserAccounts.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Delete(UserAccount user)
        {
            FashionDatabase.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            FashionDatabase.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = FashionDatabase.UserAccounts.Find(id);
            return View(obj);
        }

        [HttpGet]

        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            product.ProductImage = "~/ProductImage/" + filename;
            filename = Path.Combine(Server.MapPath("~/ProductImage/"), filename);
            product.ImageFile.SaveAs(filename);

            if(ModelState.IsValid)
            {
                FashionDatabase.Products.Add(product);
                FashionDatabase.SaveChanges();
            }
            return View();
        }


        [HttpGet]
        public ActionResult ShowProduct()
        {
            var obj = FashionDatabase.Products.ToList();
            return View(obj);
        }



        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var obj = FashionDatabase.Products.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            FashionDatabase.Entry(product).State = System.Data.Entity.EntityState.Modified;
            FashionDatabase.SaveChanges();
            return View();
        }


        
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var obj = FashionDatabase.Products.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id,Product product)
        {
            //FashionDatabase.Entry(product).State = System.Data.Entity.EntityState.Deleted;

            var obj = FashionDatabase.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            FashionDatabase.Products.Remove(obj);
            FashionDatabase.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DetailsProduct(int id)
        {
            var obj = FashionDatabase.Products.Find(id);
            return View(obj);
        }

    }
}