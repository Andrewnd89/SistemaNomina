using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> ExportarEmpleadosExcel()
        {
            var empleados = await _context.Employees.ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Empleados");

            ws.Cell(1, 1).Value = "Nro";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Apellido";
            ws.Cell(1, 4).Value = "CI";
            ws.Cell(1, 5).Value = "Correo";
            ws.Cell(1, 6).Value = "Estado";

            var headerRow = ws.Range("A1:F1");
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            headerRow.Style.Font.FontColor = XLColor.White;

            int row = 2;
            foreach (var e in empleados)
            {
                ws.Cell(row, 1).Value = e.emp_no;
                ws.Cell(row, 2).Value = e.first_name;
                ws.Cell(row, 3).Value = e.last_name;
                ws.Cell(row, 4).Value = e.ci;
                ws.Cell(row, 5).Value = e.correo;
                ws.Cell(row, 6).Value = e.activo ? "Activo" : "Inactivo";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Empleados.xlsx");
        }

        public async Task<IActionResult> ExportarSalariosExcel()
        {
            var salarios = await _context.Salaries.Include(s => s.Employee).ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Salarios");

            ws.Cell(1, 1).Value = "Empleado";
            ws.Cell(1, 2).Value = "Salario";
            ws.Cell(1, 3).Value = "Desde";
            ws.Cell(1, 4).Value = "Hasta";
            ws.Cell(1, 5).Value = "Estado";

            var headerRow = ws.Range("A1:E1");
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            headerRow.Style.Font.FontColor = XLColor.White;

            int row = 2;
            foreach (var s in salarios)
            {
                ws.Cell(row, 1).Value = s.Employee?.first_name + " " + s.Employee?.last_name;
                ws.Cell(row, 2).Value = s.salary;
                ws.Cell(row, 3).Value = s.from_date.ToShortDateString();
                ws.Cell(row, 4).Value = s.to_date.HasValue ? s.to_date.Value.ToShortDateString() : "Activo";
                ws.Cell(row, 5).Value = s.to_date == null ? "Vigente" : "Cerrado";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Salarios.xlsx");
        }
    }
}