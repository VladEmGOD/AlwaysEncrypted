using AlwaysEncrypted.DataAccess;
using AlwaysEncrypted.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlwaysEncrypted.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserProvider userProvider;

        public HomeController(ILogger<HomeController> logger, IUserProvider userProvider)
        {
            _logger = logger;
            this.userProvider = userProvider;
        }

        public IActionResult Index()
        {
            var users = userProvider.GetUsers();
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}