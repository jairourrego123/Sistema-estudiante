export interface Student {
    id: string
    userId: string
    nombre:string
    apellido:string
  }


export interface Nombre{
  nombre:string
}

export interface CrearEstudianteCommand {
  userId:string;
  nombre: string;
  apellido: string;
}

export interface EditarEstudianteCommand {
  id: string;
  nombre: string;
  apellido: string;
}