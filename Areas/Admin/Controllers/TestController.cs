using Shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Admin/Test
        public ActionResult Index()
        {
            ShopEXSEntities user = new ShopEXSEntities();
            List<User> listUser = user.Users.ToList();
            listUser.Insert(0, new User());
            return View(listUser);
        }
        //[HttpPost]
        //public ActionResult UpdateUser(User user)
        //{
        //    using (ShopEXSEntities entities = new ShopEXSEntities())
        //    {
        //        User updateUser = (from c in entities.Users where c.ID = user.ID select c).Frist
        //    }
        //}
    }
}