using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using System.Security.Cryptography;
using System.Text;

namespace SistemaNomina.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string usuario, string clave)
        {
            var hash = ComputeSha256Hash(clave);
            var user = _context.Users.FirstOrDefault(u => u.usuario == usuario && u.clave == hash);
            if (user != null)
            {
                HttpContext.Session.SetString("usuario", user.usuario);
                HttpContext.Session.SetString("rol", user.rol);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Usuario o clave incorrectos";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string ComputeSha256Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}