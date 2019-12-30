using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIInMemoryDB.Model;

namespace WebAPIInMemoryDB.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly APIContext apiContext;

        public CategoryController(APIContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = apiContext.GetCategories();
            return Ok(categories);
        }
    }
}