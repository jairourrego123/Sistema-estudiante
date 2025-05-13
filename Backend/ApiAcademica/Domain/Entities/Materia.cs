using Domain.Entities;
using Domain.Exceptions;

public class Materia : DomainBase
{
    public string Nombre { get; private set; } = null!;
    public int Creditos { get; private set; } = 3;

    public Guid ProfesorId { get; private set; }
    public Profesor Profesor { get; set; } = new();

    public Materia()
    {
        
    }
    public Materia(string nombre, Profesor profesor)
    {
        Nombre = nombre.Trim();
        Profesor = profesor;
        ProfesorId = profesor.Id;
    }

    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValorVacioException("Nombre");

        Nombre = nombre.Trim();
    }

    public void AsignarProfesor(Profesor profesor)
    {
        if (profesor.Id == Guid.Empty)
            throw new ValorVacioException("Profesor");


        ProfesorId = profesor.Id;
        Profesor = profesor;
    }
}
