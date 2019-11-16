using MyMVCDashboard.CustomAuthentication;
using MyMVCDashboard.DataAccess;
using MyMVCDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCDashboard.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            using(DashboardContext dcontext = new DashboardContext())
            {
                ViewBag.CountCustomers = dcontext.CustomerSet.Count();
                ViewBag.CountOrders = dcontext.OrderSet.Count();
                ViewBag.CountProducts = dcontext.ProductSet.Count();
            }

            return View();
        }

        public ActionResult GetDetails(string type)
        {
            List<ProductOrCustomerViewModel> result = GetProductOrCustomer(type);

            return PartialView("~/Views/Dashboard/GetDetails.cshtml", result);
        }

        public ActionResult TopCustomers()
        {
            List<TopCustomerViewModel> topCustomers = null;

            using(DashboardContext dc = new DashboardContext())
            {
                var orderByCustomer = (from o in dc.OrderSet
                                       group o by o.Customer into g
                                       orderby g.Count() descending
                                       select new
                                       {
                                           CustomerID = g.Key.ID,
                                           Count = g.Count()
                                       }).Take(5);

                topCustomers = (from c in dc.CustomerSet
                                join o in orderByCustomer
                                on c.ID equals o.CustomerID
                                select new TopCustomerViewModel
                                {
                                    CustomerName = c.Name,
                                    CustomerImage = c.Image,
                                    CustomerCountry = c.Country,
                                    CountOrder = o.Count
                                }).ToList();

            }
            return PartialView("~/Views/Dashboard/TopCustomers.cshtml", topCustomers);
        }

        public ActionResult OrderByCountry()
        {
            DashboardContext dc = new DashboardContext();

            var ordersByCountry = (from or in dc.OrderSet
                                   group or by or.Customer.Country into g
                                   orderby g.Count() descending
                                   select new
                                   {
                                       Country = g.Key,
                                       CountOrders = g.Count()
                                   }).ToList();

            return Json(new { result = ordersByCountry }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomersByCountry()
        {
            DashboardContext dc = new DashboardContext();

            var customerByCountry = (from cu in dc.CustomerSet
                                      group cu by cu.Country into g
                                      orderby g.Count() descending
                                      select new
                                      {
                                          Country = g.Key,
                                          CountCustomers = g.Count()
                                      }).ToList();
            return Json(new { result = customerByCountry }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrdersByCustomer()
        {
            DashboardContext dc = new DashboardContext();

            var orderByCustomer = (from o in dc.OrderSet
                                   group o by o.Customer.ID into g
                                   select new
                                   {
                                       Name = (from c in dc.CustomerSet
                                              where c.ID == g.Key
                                              select c.Name).FirstOrDefault(),

                                       CountOrders = g.Count()
                                   }).ToList();

            return Json(new { result = orderByCustomer }, JsonRequestBehavior.AllowGet);
        }

        private List<ProductOrCustomerViewModel> GetProductOrCustomer(string type)
        {
            List<ProductOrCustomerViewModel> productOrCustomerList = null;

            if(type == "customers")
            {
                using(DashboardContext dcontext = new DashboardContext())
                {
                    productOrCustomerList = dcontext.CustomerSet.Select(c => new ProductOrCustomerViewModel
                    {
                        Name = c.Name,
                        Image = c.Image,
                        TypeOrCountry = c.Country,
                        Type = "Customers"
                    }).ToList();
                }
            }
            else if(type == "products")
            {
                using(DashboardContext dcontext = new DashboardContext())
                {
                    productOrCustomerList = dcontext.ProductSet.Select(p => new ProductOrCustomerViewModel
                    {
                        Name = p.ProductName,
                        Image = p.ProductImage,
                        TypeOrCountry = p.ProductType,
                        Type = p.ProductType
                    }).ToList();
                }
            }

            return productOrCustomerList;
        }
    }
}