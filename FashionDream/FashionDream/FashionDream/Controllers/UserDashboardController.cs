using FashionDream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionDream.Controllers
{
    public class UserDashboardController : Controller
    {
        FashionDreamEntities FashionDatabase = new FashionDreamEntities();
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
        public ActionResult DetailsProduct(int id)
        {
            var obj = FashionDatabase.Products.Find(id);
            return View(obj);
        }
    }
}