using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Web.Models;
using Movies.Web.Pages;
using Ooui.AspNetCore;
using Xamarin.Forms;

namespace Movies.Web.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var page =  new HomeView();
            var navigationPage = new NavigationPage(page);
            var element = navigationPage.GetOouiElement();
            return new ElementResult(element, "Movies Ooui Forms");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
