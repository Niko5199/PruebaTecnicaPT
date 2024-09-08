﻿using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaPT.Models.Dtos
{
    public class EmployeeDto
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage = "The firstName is required")]
        [MaxLength(50, ErrorMessage = "The size of firsName is to long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The lastName is required")]
        [MaxLength(50, ErrorMessage = "The size of LastName is to long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The salary is required")]
        public double Salary { get; set; }
    }
}
