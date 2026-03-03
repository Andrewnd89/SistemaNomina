using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Controllers
{
    public class TitulosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TitulosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var titulos = await _context.Titles
                .Include(t => t.Employee)
                .ToListAsync();
            return View(titulos);
        }

        public IActionResult Crear()
        {
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Title titulo)
        {
            if (ModelState.IsValid)
            {
                _context.Titles.Add(titulo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            return View(titulo);
        }

        public async Task<IActionResult> Terminar(int id)
        {
            var titulo = await _context.Titles.FindAsync(id);
            if (titulo != null)
            {
                titulo.to_date = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}