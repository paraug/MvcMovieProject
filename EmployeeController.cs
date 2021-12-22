using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDB employeeDB = new EmployeeDB();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(employeeDB.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insert(Employee emp)
        {
            return Json(employeeDB.Insert(emp), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            var Employee = employeeDB.ListAll().Find(x => x.Id.Equals(ID));
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Employee emp)
        {
            return Json(employeeDB.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(employeeDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                using (DB_Entities db = new DB_Entities())
                {
                    var obj = db.UserProfiles.Where(a => a.UserName.Equals(userProfile.UserName) && a.Password.Equals(userProfile.Password)).FirstOrDefault();                    
                    if (obj != null)
                    {
                        Session["UserId"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username and password");
                    }
                }
            }
            return View(userProfile);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Employee");
        }
        public ActionResult UserDashBoard()
        {
            if(Session["UserId"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public JsonResult EmployeeList()
        {
            return Json(employeeDB.ListEmployeeName(), JsonRequestBehavior.AllowGet);
        }        

    }
}