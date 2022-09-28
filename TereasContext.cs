using Microsoft.EntityFrameworkCore;
using proyectoEF.Models;

namespace proyectoEF
{
    public class TareasContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e811"), Nombre = "Actividades pendientes", Peso = 20});
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e812"), Nombre = "Actividades personales", Peso = 30});


            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");                                             // indica que es una tanbla
                categoria.HasKey(p => p.CategoriaId);                                        // indica que es la llave primaria
                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);            // indica que es un campo obligatorio y su tamaño maximo es 150
                categoria.Property(p => p.Descripcion).IsRequired(false);                   // indica que es un campo opcional y quitarle no null
                categoria.Property(p => p.Peso);                                             // indica que es un campo opcional
                categoria.HasData(categoriasInit);                                          // indica un vector de categorias o lista de datos
            });

            List<Tarea> tareasInit = new List<Tarea>();
            tareasInit.Add(new Tarea() {TareaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e000"), CategoriaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e811"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios", FechaCreacion = DateTime.Now});
            tareasInit.Add(new Tarea() {TareaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e001"), CategoriaId = Guid.Parse("c11dc9bb-3c11-4b44-847e-da716315e812"), PrioridadTarea = Prioridad.Alta, Titulo = "Pago de arriendo", FechaCreacion = DateTime.Now});


            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");                                                     // indica que es una tabla
                tarea.HasKey(p => p.TareaId);                                                // indica que es la llave primaria
                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId); // indica que una tarea tiene una categoria y una categoria tiene muchas tareas
                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);                // indica que es un campo obligatorio y su tamaño máximo es 200
                tarea.Property(p => p.Descripcion).IsRequired(false);                       // indica que es un campo opcional
                tarea.Property(p => p.PrioridadTarea);                                       // indica que es un campo opcional
                tarea.Property(p => p.FechaCreacion);                                        // indica que es un campo opcional
                tarea.Ignore(p => p.Resumen);                                                // indica que ignora el campo en la base de datos
                tarea.Property(p => p.Docente).IsRequired(false);                                              // indica que es un campo opcional
                tarea.HasData(tareasInit);                                                  // indica un vector de categorias o lista de datos
            });
        }
    }
}