using Domain.Entities;
using Domain.Exceptions;
using Shared.Const;

public class Profesor : DomainBase
{
    public string Nombre { get;  set; } 
    public List<Materia> Materias { get;  set; } = new();

    public Profesor()
    {
        
    }
    public Profesor(string nombre)
    {
        CambiarNombre(nombre);
    }

    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValorVacioException("Nombre") ;

        Nombre = nombre.Trim();
    }

    public void AgregarMateria(Materia materia)
    {
        if (Materias.Count >= Const.LimiteMateriasXProfesor)
            throw new LimiteMateriasDictadasExceptions(Const.LimiteMateriasXProfesor);

        Materias.Add(materia);
    }

    public void RemoverMateria(Guid materiaId)
    {
        Materia? materia = Materias.FirstOrDefault(m => m.Id == materiaId)
            ?? throw new NoEncontradoException("Materia");

        Materias.Remove(materia);
    }
}
