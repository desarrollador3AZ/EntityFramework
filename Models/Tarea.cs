using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoEF.Models
{
    public class Tarea
    {
        //[Key]  atributos
        public Guid TareaId { get; set; }
        //[ForeignKey("CategoriaId")]  atributos
        public Guid CategoriaId { get; set; }
        //[Required]  atributos
        //[MaxLength(200)]  atributos
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Prioridad PrioridadTarea { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual Categoria Categoria { get; set; }
        //[NotMapped]  atributos
        public string Resumen { get; set; }
        public string Docente { get; set; }
    }

    public enum Prioridad
    {
        Baja,
        Media,
        Alta
    }
}