using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaNomina.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string buscar)
        {
            var empleados = _context.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(buscar))
                empleados = empleados.Where(e => e.first_name.Contains(buscar) || e.last_name.Contains(buscar) || e.correo.Contains(buscar));
            ViewBag.Buscar = buscar;
            return View(await empleados.ToListAsync());
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                emp.activo = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Ver(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }
    }
}