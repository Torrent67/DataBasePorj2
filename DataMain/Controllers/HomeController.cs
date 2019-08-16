using Microsoft.AspNetCore.Mvc;

namespace DataBase.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}