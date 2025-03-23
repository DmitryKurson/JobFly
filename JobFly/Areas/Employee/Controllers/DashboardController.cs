using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFly.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
