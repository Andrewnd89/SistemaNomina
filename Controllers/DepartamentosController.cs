using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaNomina.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string buscar)
        {
            var deps = _context.Departments.AsQueryable();
            if (!string.IsNullOrEmpty(buscar))
                deps = deps.Where(d => d.dept_name.Contains(buscar));
            ViewBag.Buscar = buscar;
            return View(await deps.ToListAsync());
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(Department dep)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dep);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dep);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var dep = await _context.Departments.FindAsync(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Department dep)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(dep);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dep);
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            var dep = await _context.Departments.FindAsync(id);
            if (dep != null)
            {
                dep.activo = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}