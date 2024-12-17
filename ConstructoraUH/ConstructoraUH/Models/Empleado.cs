using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructoraUH.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CarnetUnico { get; set; }

        [Required(ErrorMessage = "El nombre completo es requerido")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = "San José";

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Range(250000, 500000, ErrorMessage = "El salario debe estar entre 250,000 y 500,000")]
        [DataType(DataType.Currency)]
        public decimal Salario { get; set; } = 250000;

        [Required(ErrorMessage = "La categoría laboral es requerida")]
        [Display(Name = "Categoría Laboral")]
        public CategoriaLaboral CategoriaLaboral { get; set; }

        public ICollection<Asignacion> Asignaciones { get; set; }
    }

    public enum CategoriaLaboral
    {
        Administrador,
        Operario,
        Peon
    }
}