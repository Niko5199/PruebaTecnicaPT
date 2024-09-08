using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaPT.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public double Salary { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }
    }
}
