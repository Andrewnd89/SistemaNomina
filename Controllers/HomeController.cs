using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEmpleados = await _context.Employees.CountAsync(e => e.activo);
            ViewBag.TotalDepartamentos = await _context.Departments.CountAsync(d => d.activo);
            ViewBag.TotalSalarios = await _context.Salaries.CountAsync(s => s.to_date == null);
            ViewBag.TotalTitulos = await _context.Titles.CountAsync(t => t.to_date == null);
            return View();
        }
    }
}