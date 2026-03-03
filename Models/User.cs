using System.ComponentModel.DataAnnotations;

namespace SistemaNomina.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int emp_no { get; set; }

        [Required]
        public string usuario { get; set; } = string.Empty;

        [Required]
        public string clave { get; set; } = string.Empty;

        [Required]
        public string rol { get; set; } = "RRHH";
    }
}