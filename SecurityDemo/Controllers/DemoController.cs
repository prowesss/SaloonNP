using Microsoft.AspNetCore.Mvc;

namespace SecurityDemo.Controllers
{
    [Route ("Demo")]
    public class DemoController : Controller
    {
        [Route("Demo")]
        [Route("")]
        [Route("-/")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
