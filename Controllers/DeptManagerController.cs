using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Controllers
{
    public class DeptManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeptManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var managers = await _context.DeptManagers
                .Include(m => m.Employee)
                .Include(m => m.Department)
                .ToListAsync();
            return View(managers);
        }

        public IActionResult Crear()
        {
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            ViewBag.Departamentos = _context.Departments.Where(d => d.activo).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DeptManager manager)
        {
            if (ModelState.IsValid)
            {
                var anterior = await _context.DeptManagers
                    .Where(m => m.dept_no == manager.dept_no && m.to_date == null)
                    .FirstOrDefaultAsync();

                if (anterior != null)
                {
                    anterior.to_date = DateTime.Now;
                    _context.DeptManagers.Update(anterior);
                }

                _context.DeptManagers.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            ViewBag.Departamentos = _context.Departments.Where(d => d.activo).ToList();
            return View(manager);
        }

        public async Task<IActionResult> Terminar(int id)
        {
            var manager = await _context.DeptManagers.FindAsync(id);
            if (manager != null)
            {
                manager.to_date = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}