using System.ComponentModel.DataAnnotations;

namespace SistemaNomina.Models
{
    public class Employee
    {
        [Key]
        public int emp_no { get; set; }

        [Required]
        public string ci { get; set; } = string.Empty;

        [Required]
        public DateTime birth_date { get; set; }

        [Required]
        public string first_name { get; set; } = string.Empty;

        [Required]
        public string last_name { get; set; } = string.Empty;

        [Required]
        public string gender { get; set; } = string.Empty;

        [Required]
        public DateTime hire_date { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; } = string.Empty;

        public bool activo { get; set; } = true;
    }
}