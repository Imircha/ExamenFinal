using System.ComponentModel.DataAnnotations;

namespace ConstructoraUH.Models
{
    public class Proyecto
    {
        [Key]
        public int CodigoProyecto { get; set; }

        [Required(ErrorMessage = "El nombre del proyecto es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Finalización")]
        public DateTime? FechaFinalizacion { get; set; }

        public ICollection<Asignacion> Asignaciones { get; set; }
    }
}