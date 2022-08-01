using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        [Display(Name = "Cidade Id")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!!")]
        [Display(Name = "Cidade")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!!")]
        [Display(Name = "Departamento")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um Departamento")]
        public int DepartamentsId { get; set; }

        public virtual Departaments Departament { get; set; }
        public virtual ICollection<Company> Company { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}