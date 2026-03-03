using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Controllers
{
    public class DeptEmpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeptEmpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var asignaciones = await _context.DeptEmps
                .Include(d => d.Employee)
                .Include(d => d.Department)
                .ToListAsync();
            return View(asignaciones);
        }

        public IActionResult Crear()
        {
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            ViewBag.Departamentos = _context.Departments.Where(d => d.activo).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DeptEmp deptEmp)
        {
            if (ModelState.IsValid)
            {
                var anterior = await _context.DeptEmps
                    .Where(d => d.emp_no == deptEmp.emp_no && d.to_date == null)
                    .FirstOrDefaultAsync();

                if (anterior != null)
                {
                    anterior.to_date = DateTime.Now;
                    _context.DeptEmps.Update(anterior);
                }

                _context.DeptEmps.Add(deptEmp);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            ViewBag.Departamentos = _context.Departments.Where(d => d.activo).ToList();
            return View(deptEmp);
        }

        public async Task<IActionResult> Terminar(int id)
        {
            var asignacion = await _context.DeptEmps.FindAsync(id);
            if (asignacion != null)
            {
                asignacion.to_date = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}