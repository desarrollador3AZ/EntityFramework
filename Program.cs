using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoEF;
using proyectoEF.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("conTareas"));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext DbContext) =>
{
    DbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + DbContext.Database.IsInMemory());
});
// Consultar
app.MapGet("/api/tareas", async ([FromServices] TareasContext DbContext) =>
{
    return Results.Ok(DbContext.Tareas.Include(p => p.Categoria));
});
// Agregar
app.MapPost("/api/tareas", async ([FromServices] TareasContext DbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await DbContext.AddAsync(tarea);                // dos maneras para agregar el registro
    // await DbContext.Tareas.AddAsync(tarea);
    await DbContext.SaveChangesAsync();

    return Results.Ok();
});
// Actualizar
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext DbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = DbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await DbContext.SaveChangesAsync();

        return Results.Ok();
    }
    return Results.NotFound();
});
// Eliminar
app.MapDelete("/api/tareas/{id}", async([FromServices] TareasContext DbContext, [FromRoute] Guid id)=>
{
    var tareaActual = DbContext.Tareas.Find(id);

    if(tareaActual != null)
    {
        DbContext.Remove(tareaActual);
        await DbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
