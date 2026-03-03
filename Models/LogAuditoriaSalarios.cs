using System.ComponentModel.DataAnnotations;

namespace SistemaNomina.Models
{
    public class LogAuditoriaSalarios
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string usuario { get; set; } = string.Empty;

        [Required]
        public DateTime fechaActualizacion { get; set; }

        [Required]
        public string DetalleCambio { get; set; } = string.Empty;

        [Required]
        public decimal salario { get; set; }

        [Required]
        public int emp_no { get; set; }
    }
}