using JobFly.Areas.Employer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Policy = "EmployerOnly")]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
