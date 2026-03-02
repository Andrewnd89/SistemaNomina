using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaNomina.Models
{
    public class DeptManager
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int emp_no { get; set; }

        [Required]
        public int dept_no { get; set; }

        [Required]
        public DateTime from_date { get; set; }

        public DateTime? to_date { get; set; }

        [ForeignKey("emp_no")]
        public Employee? Employee { get; set; }

        [ForeignKey("dept_no")]
        public Department? Department { get; set; }
    }
}