
-- ========================================
-- EstudianteQueryRepository
-- ========================================

-- Obtener estudiante por UserId, incluyendo inscripciones y materias
CREATE OR ALTER PROCEDURE paObtenerEstudiantePorUserId
    @userId NVARCHAR(100)
AS
BEGIN
    SELECT e.*, i.Id, i.MateriaId, i.FechaGrabacion,
           m.Id, m.Nombre, m.Creditos, m.ProfesorId
    FROM Estudiantes e
    LEFT JOIN Inscripciones i ON i.EstudianteId = e.Id
    LEFT JOIN Materias m      ON m.Id = i.MateriaId
    WHERE e.UserId = @userId;
END
GO

-- Obtener estudiante por Id
CREATE OR ALTER PROCEDURE paObtenerEstudiantePorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT Id, UserId, Nombre, Apellido, FechaGrabacion
    FROM Estudiantes
    WHERE Id = @Id;
END
GO

-- Listar todos los estudiantes
CREATE OR ALTER PROCEDURE paObtenerTodosLosEstudiantes
AS
BEGIN
    SELECT * FROM Estudiantes;
END
GO

-- Listar nombres de compañeros en una materia (excluyendo al usuario actual)
CREATE OR ALTER PROCEDURE paListarCompañerosDeMateria
    @materiaId UNIQUEIDENTIFIER,
    @userId NVARCHAR(100)
AS
BEGIN
    SELECT DISTINCT e.Nombre + ' ' + e.Apellido
    FROM Inscripciones i
    JOIN Estudiantes e ON e.Id = i.EstudianteId
    WHERE i.MateriaId = @materiaId
      AND e.UserId <> @userId;
END
GO

-- ========================================
-- MateriaQueryRepository
-- ========================================

-- Obtener materia por Id
CREATE OR ALTER PROCEDURE paObtenerMateriaPorId
    @id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Materias WHERE Id = @id;
END
GO

-- Obtener todas las materias con profesor asignado
CREATE OR ALTER PROCEDURE paObtenerTodasLasMaterias
AS
BEGIN
    SELECT 
        m.Id, m.Nombre, m.Creditos, m.ProfesorId,
        p.Id, p.Nombre
    FROM Materias m
    JOIN Profesores p ON p.Id = m.ProfesorId;
END
GO

-- Obtener varias materias por lista de Ids
CREATE OR ALTER  PROCEDURE paObtenerMateriasPorIds
    @Ids GuidList READONLY
AS
BEGIN
    SELECT * FROM Materias WHERE Id IN (SELECT Id FROM @ids);
END
GO

-- ========================================
-- ProfesorQueryRepository
-- ========================================

-- Obtener profesor por Id con sus materias
CREATE OR ALTER PROCEDURE paObtenerProfesorPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT p.*, m.Id, m.Nombre, m.Creditos, m.ProfesorId
    FROM Profesores p
    LEFT JOIN Materias m ON m.ProfesorId = p.Id
    WHERE p.Id = @Id;
END
GO

-- Obtener todos los profesores
CREATE OR ALTER PROCEDURE paObtenerTodosLosProfesores
AS
BEGIN
    SELECT * FROM Profesores;
END
GO
