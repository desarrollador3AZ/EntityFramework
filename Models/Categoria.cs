using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace proyectoEF.Models
{
    public class Categoria
    {
        // [Key]  atributos
        public Guid CategoriaId { get; set; }
        // [Required]  atributos
        // [MaxLength(150)]  atributos
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}