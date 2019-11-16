using MyMVCDashboard.CustomAuthentication;
using MyMVCDashboard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCDashboard.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class CustomerInformationController : Controller
    {
        // GET: CustomerInformation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            using(DashboardContext dc = new DashboardContext())
            {
                var customerList = dc.CustomerSet.Select(c => new
                {
                    c.ID,
                    c.Name,
                    c.Country,
                    c.Email,
                    c.Phone,
                    c.Image
                }).ToList();
                return Json(new { data = customerList }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}