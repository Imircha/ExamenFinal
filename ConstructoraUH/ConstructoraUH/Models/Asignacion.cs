using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructoraUH.Models
{
    public class Asignacion
    {
        [Key]
        public int AsignacionId { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Asignación")]
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        [ForeignKey("EmpleadoId")]
        public Empleado Empleado { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }
    }
}