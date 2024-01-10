using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
