using FashionDream.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




using System.Data;

using System.Web.UI.WebControls;


namespace FashionDream.Controllers
{
    public class UserDashboardController : Controller
    {
        public static int summation = 0;
        public static int Product_id = 0;
        public static int Variation_id = 0;
        public static int payment_id = 0;
        int the_id  = FashionDream.Controllers.UserController.UID;
        FashionDream1Entities12 FashionDatabase = new FashionDream1Entities12();
        // GET: UserDashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MainPage()
        {
            var obj = FashionDatabase.Products.ToList();
            return View(obj);
        }
        [HttpGet]
        public ActionResult DetailsProduct()
        {
            ViewBag.product_id_forDetails = Product_id;
            var obj = FashionDatabase.Products.Find(Product_id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult DetailsProduct([Bind(Include = "ID, ProductID, VariationID, TotalCost, Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                FashionDatabase.Carts.Add(cart);
                FashionDatabase.SaveChanges();
                return RedirectToAction("CartItem", "UserDashboard");
            }
            return View();
        }

        List<Variation> variations;
       

        [HttpGet]
        public ActionResult ProductShow(int id)
        {
            var obj = FashionDatabase.Products.Find(id);
            Product_id = id;
            ViewBag.Product_id = id;
            return View(obj);
        }
        [HttpPost]
        public ActionResult ProductShow([Bind(Include = "Id, ProductId, Color, Size")] Variation variation)
        {
            if (ModelState.IsValid)
            {
                FashionDatabase.Variations.Add(variation);
                //Variation_id = variation.VariationID;
                FashionDatabase.SaveChanges();

                variations = FashionDatabase.Variations.Where(temp => temp.ID == the_id &&
                temp.ProductID == Product_id && temp.Color==variation.Color 
                && temp.Size==variation.Size).ToList();

                Variation_id = variations[0].VariationID;

                return RedirectToAction("DetailsProduct", "UserDashboard");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Catagory()
        {
            var obj = FashionDatabase.Products.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult CartItem()
        {
            

            var cartmodel = from c in FashionDatabase.Carts
                            join p in FashionDatabase.Products on c.ProductID equals p.ProductID join
                            v in FashionDatabase.Variations on c.VariationID equals v.VariationID where c.ID == FashionDream.Controllers.UserController.UID 
                            select new CartJoin { cart = c, product = p ,variation =v};
            return View(cartmodel);
            
        }

        public ActionResult CartDetailsProduct(int id)
        {
            var obj = FashionDatabase.Carts.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult CartDetailsProduct(int id,Cart cart)
        {
            /*FashionDatabase.Entry(cart).State = System.Data.Entity.EntityState.Deleted;
            FashionDatabase.SaveChanges();
            return View();*/

            var obj = FashionDatabase.Carts.Where(temp => temp.CartID == id).FirstOrDefault();
            FashionDatabase.Carts.Remove(obj);
            FashionDatabase.SaveChanges();
            return RedirectToAction("CartItem");
        }
        /* [HttpPost]
         public ActionResult CartItem(int id)
         {


                 var it = FashionDatabase.Carts.Find(id);
                 FashionDatabase.Carts.Remove(it);
                 FashionDatabase.SaveChanges();

                 return RedirectToAction("CartItem", "UserDashboard");


         }
        */
        [HttpGet]
        public ActionResult Payment()
        {
            var cartmodel = from c in FashionDatabase.Carts
                            join p in FashionDatabase.Products on c.ProductID equals p.ProductID
                            join
                            v in FashionDatabase.Variations on c.VariationID equals v.VariationID
                            where c.ID == FashionDream.Controllers.UserController.UID
                            select new CartJoin { cart = c, product = p, variation = v };
            return View(cartmodel);
        }
        [HttpPost]
        public ActionResult Payment([Bind(Include = "ID, BillNo, Method, PaymentTime, PaymentStatus")] Payment payment)
        {

            string Bill = Request["BillNo"];
            int bill = Convert.ToInt32(Bill);

            if(ModelState.IsValid)
            {
                FashionDatabase.Payments.Add(payment);
                FashionDatabase.SaveChanges();


                /* string sqql = "delete * from Cart where ID = '"+FashionDream.Controllers.UserController.UID+"'";
                 FashionDatabase.Carts.SqlQuery(sqql).ToList();*/
                var obj = FashionDatabase.Carts.Where(temp => temp.ID == FashionDream.Controllers.UserController.UID).ToList();
                FashionDatabase.Carts.RemoveRange(obj);
                FashionDatabase.SaveChanges();
                /*payments = FashionDatabase.Payments.Where(temp => temp.ID == the_id ).ToList();
                payment_id = payments[0].PaymentID;*/

                return RedirectToAction("MainPage", "UserDashboard");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OrderTable()
        {
            var obj = FashionDatabase.Payments.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feedback([Bind(Include ="ID, Name, Email, Review")] Feedback feedback)
        {
            if(ModelState.IsValid)
            {
                FashionDatabase.Feedbacks.Add(feedback);
                FashionDatabase.SaveChanges();
                return RedirectToAction("MainPage","UserDashboard");
            }

            return View();
        }
        public ActionResult FeedbackShow()
        {
            var obj = FashionDatabase.Feedbacks.ToList();
            return View(obj);
        }

        public ActionResult UserProfile()
        {
            var obj = FashionDatabase.UserAccounts.Find(FashionDream.Controllers.UserController.UID);
            return View(obj);
        }
        [HttpGet]
        public ActionResult UserProfileEdit(int id)
        {
            var obj = FashionDatabase.UserAccounts.Find(FashionDream.Controllers.UserController.UID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult UserProfileEdit(UserAccount user)
        {
            FashionDatabase.Entry(user).State = System.Data.Entity.EntityState.Modified;
            FashionDatabase.SaveChanges();
            return View();
        }

    }
}