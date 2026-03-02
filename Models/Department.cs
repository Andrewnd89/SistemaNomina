using System.ComponentModel.DataAnnotations;

namespace SistemaNomina.Models
{
    public class Department
    {
        [Key]
        public int dept_no { get; set; }

        [Required]
        public string dept_name { get; set; } = string.Empty;

        public bool activo { get; set; } = true;
    }
}