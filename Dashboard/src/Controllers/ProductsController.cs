using MyMVCDashboard.DataAccess;
using MyMVCDashboard.Entities;
using MyMVCDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCDashboard.Controllers
{
    public class ProductsController : Controller
    {
        public static List<ProductsViewModel> productList = new List<ProductsViewModel>();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductList(int id)
        {
            Session["CustomerID"] = id;

            return View();
        }

        [HttpGet]
        public ActionResult GetProductsByCategory(string category)
        {
            using(DashboardContext dc = new DashboardContext())
            {
                var productList = dc.ProductSet.Where(p => p.ProductType.ToLower() == category.ToLower()).Select(p => new ProductsViewModel
                {
                    ProductID = p.ID,
                    ProductName = p.ProductName,
                    ProductType = p.ProductType,
                    ProductImage = p.ProductImage,
                    UnitPriceProduct = p.UnitPrice,
                    UnitsInStock = p.UnitsinStock
                }).ToList();
                return PartialView("~/Views/Products/GetProductsByCategory.cshtml", productList);       
            }
        }

        [HttpPost]
        public ActionResult ShoppingCart(ProductsViewModel product)
        {
            string message = string.Empty;
            if(product != null)
            {
                productList.Add(product);
                message = "Product has been added succesfully";
            }
            else
                message = "Somethin went wrong";

            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayShoppingCart()
        {
            List<ProductsViewModel> myproductList = productList;

            return PartialView("~/Views/Products/DisplayShoppingCart.cshtml", myproductList);
        }

        public ActionResult AddOrder(int[] arrIDProduct, int[] arrQteProduct)
        {
            int countProduct = arrIDProduct.Length;
            int customerID = (int)Session["CustomerID"];
            bool statusTran = false;


            using(DashboardContext dcontext = new DashboardContext())
            {
                using(DbContextTransaction dtransaction = dcontext.Database.BeginTransaction())
                {
                    try
                    {
                        Customer customer = dcontext.CustomerSet.Find(customerID);

                        if (customer != null)
                        {
                            customer.Orders.Add(new Order { OrderDate = DateTime.Now });
                        }

                        Order orderSelected = customer.Orders.LastOrDefault();

                        if (orderSelected != null)
                        {
                            for (int i = 0; i < countProduct; i++)
                            {
                                Product selectedProduct = dcontext.ProductSet.Find(arrIDProduct[i]);
                                orderSelected.OrderDetails.Add(new OrderDetails { Product = selectedProduct, Quantity = arrQteProduct[i] });
                            }
                        }

                        dcontext.SaveChanges();

                        dtransaction.Commit();

                        if(countProduct > 0)
                        {
                            statusTran = true;
                            productList.Clear();
                        }
                    }
                    catch (Exception)
                    {
                        dtransaction.Rollback();
                    }
                }
            }

            return Json(new { data = statusTran }, JsonRequestBehavior.AllowGet);
        }
    }
}