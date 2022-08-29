using FashionDream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionDream.Controllers
{
    public class UserController : Controller
    {

        FashionDream1Entities12 FashionDatabase = new FashionDream1Entities12();

        public static int UID = 0;
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include ="FirstName, Email, Password")] UserAccount user)
        {

            List<UserAccount> userAccounts = FashionDatabase.UserAccounts.Where(temp => temp.FirstName == user.FirstName &&
            temp.Email == user.Email && temp.Password == user.Password).ToList();

                while (userAccounts.Count > 0)
                {

                    int uid = userAccounts[0].ID;
                    UID = uid;
                    return RedirectToAction("MainPage", "UserDashboard");
                }
          
          
                ViewBag.missmatch ="Miss Match in Email or Password";
            
           // ModelState.AddModelError("", "Miss Match in Email or Password");

            return View();
        }


       
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Registration([Bind(Include = "FirstName, LastName,Address, Phone, Email, Password, DateOfBirth")] UserAccount user)
        {
            if(ModelState.IsValid)
            {
                FashionDatabase.UserAccounts.Add(user);
                FashionDatabase.SaveChanges();
                return View();
            }
           
            return View();
        }


        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin adminLogin)
        {
            if((adminLogin.Name.Equals("FashionDream"))&&(adminLogin.Password.Equals("123456789"))&&(adminLogin.Email.Equals("admin777@gmail.com") ))
            {
                return RedirectToAction("Display","Admin");
            }
            else
            {
                ViewBag.Adminmissmatch = "Miss Match in Email or Password";
            }
           
            return View();
        }

        [HttpGet]
        public ActionResult DemoMainPage()
        {
            var obj = FashionDatabase.Products.ToList();
            return View(obj);
        }
        [HttpGet]
        public ActionResult DemoCatagory()
        {
            var obj = FashionDatabase.Products.ToList();
            return View(obj);
        }
    }
}