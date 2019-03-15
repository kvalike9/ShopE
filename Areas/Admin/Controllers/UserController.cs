using Shop.Models.DAO;
using Shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
            //return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDAO();
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                    int res = dao.Create(user.UserName, user.Password, user.Name, user.Email, user.Address, user.Phone, user.Status);
                    //int id = dao.Insert(user);
                    if (res > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user thành công");
                    }
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDAO();
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                    int res = dao.Update(user.ID, user.Password, user.Name, user.Address, user.Email, user.Phone, user.Status);
                    //var id = dao.Update2(user);
                    if (res > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật user thành công");
                    }
                }
                return View(user);
            }
            catch
            {
                return View();
            }
          
        }

    }
}