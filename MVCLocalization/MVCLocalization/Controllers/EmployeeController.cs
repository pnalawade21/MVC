using MVCLocalization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLocalization.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Employee()
        {
            EmployeeModel empModel = new EmployeeModel();
            return View(empModel);
        }

        [HttpPost]
        public ActionResult Employee(EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                //Do something
            }
            return View(model);
        }
    }
}