using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        [Key]

        public int UserId { get; set; }


        [Required(ErrorMessage = "O campo E-Mail é requerido!!")]
        [MaxLength(250, ErrorMessage = "O campo E-Mail recebe no máximo 250 caracteres")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Index("User_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!!")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Nome")]
        public string FirtName { get; set; }

        [Required(ErrorMessage = "O campo SobreNome é requerido!!")]
        [MaxLength(50, ErrorMessage = "O campo SobreNome recebe no máximo 50 caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo Telefone é requerido!!")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Telefone")]
        //[Index("Departament_Name_Index", IsUnique = true)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é requerido!!")]
        [MaxLength(100, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requerido!!")]
        [Display(Name = "Departamento")]
        public int DepartamentsId { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requerido!!")]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requerido!!")]
        [Display(Name = "Companhia")]
        public int CompanyId { get; set; }

        [Display(Name = "Usuário")]
        public string FullName { get { return string.Format("{0} {1}", FirtName, LastName); } }

        public virtual Departaments Departments { get; set; }
        public virtual City Cities { get; set; }

        public virtual Company Company { get; set; }
    }
}