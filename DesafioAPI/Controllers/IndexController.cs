using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.Controllers
{
    [Route("/apionline")]
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
