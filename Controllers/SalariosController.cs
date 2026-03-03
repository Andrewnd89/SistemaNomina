using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaNomina.Controllers
{
    public class SalariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salarios = await _context.Salaries
                .Include(s => s.Employee)
                .ToListAsync();
            return View(salarios);
        }

        public IActionResult Crear()
        {
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Salary sal, string usuario)
        {
            if (ModelState.IsValid)
            {
                var anterior = await _context.Salaries
                    .Where(s => s.emp_no == sal.emp_no && s.to_date == null)
                    .FirstOrDefaultAsync();

                if (anterior != null)
                {
                    anterior.to_date = DateTime.Now;
                    _context.Salaries.Update(anterior);
                }

                _context.Salaries.Add(sal);

                _context.LogAuditoriaSalarios.Add(new LogAuditoriaSalarios
                {
                    usuario = usuario ?? "sistema",
                    fechaActualizacion = DateTime.Now,
                    DetalleCambio = "Nuevo salario registrado",
                    salario = sal.salary,
                    emp_no = sal.emp_no
                });

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Empleados = _context.Employees.Where(e => e.activo).ToList();
            return View(sal);
        }
    }
}