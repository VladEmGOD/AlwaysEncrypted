using AlwaysEncrypted.DataAccess;
using AlwaysEncrypted.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserDTO user)
        {
            userProvider.AddUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            var keys = new KeysModel();

            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.KeySize = 256;
                aesAlgorithm.GenerateKey();

                byte[] masterKey = aesAlgorithm.Key;
                keys.MaskterKey = BitConverter.ToString(masterKey).Replace("-", "");
                keys.MaskterKeyBase64 = Convert.ToBase64String(masterKey);

                aesAlgorithm.GenerateKey();
                byte[] encryptionKey = aesAlgorithm.Key;
                keys.EncryptionKey = BitConverter.ToString(encryptionKey).Replace("-", ""); ;
                keys.EncryptionKeyBase64 = Convert.ToBase64String(encryptionKey);

                aesAlgorithm.Key = masterKey;
                byte[] encryptedEncryptionKey = aesAlgorithm.EncryptEcb(encryptionKey, PaddingMode.None);

                keys.EncryptedEncryptionKey = BitConverter.ToString(encryptedEncryptionKey).Replace("-", "");
                keys.EncryptedEncryptionKeyBase64 = Convert.ToBase64String(encryptedEncryptionKey);
            }

            return View(keys);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}