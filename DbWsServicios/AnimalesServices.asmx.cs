using DbWsServicios.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DbWsServicios
{
    /// <summary>
    /// Descripción breve de AnimalesServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class AnimalesServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod(Description = "Consultar datos de los animales")]

        public List<Animal> ObtenerAnimales()
        {
            using(var conexion= new AnimalesEntities())
            {
                var consulta = from c in conexion.Animals select c;

                return consulta.ToList();
            }
        }

        [WebMethod(Description = "Insertar un nuevo animal")]
        public bool InsertarAnimal(string nombre, string especie, int edad, string color, string ubicacion)
        {
            using (var conexion = new AnimalesEntities())
            {
                try
                {
                    // Crea un nuevo objeto Animal y establece sus propiedades
                    var nuevoAnimal = new Animal
                    {
                        Id = Guid.NewGuid(), // Genera un nuevo GUID para el ID osea un ID totalmente aleatoria
                        Nombre = nombre, //almacena lo datos que se teclean en el web service
                        Especie = especie,
                        Edad = edad,
                        Color = color, // Puedes establecer el valor de manera nula si fuera necesario
                        Ubicacion = ubicacion
                    };

                    // Agrega el nuevo animal al conjunto de entidades Animals
                    conexion.Animals.Add(nuevoAnimal);

                    // Guarda los cambios en la base de datos
                    conexion.SaveChanges();

                    // Devuelve true si la inserción fue exitosa
                    return true;
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }

        [WebMethod(Description = "Eliminar un animal por nombre")]
        public bool EliminarAnimalPorNombre(string nombre)
        {
            using (var conexion = new AnimalesEntities())
            {
                try
                {
                    // Busca el animal por su nombre
                    var animalAEliminar = conexion.Animals.FirstOrDefault(a => a.Nombre == nombre);//Realiza una consulta en la tabla "Animals" utilizando LINQ to Entities para encontrar el primer registro que cumple con una condición específica.

                    // Si se encontró el animal, elimínalo
                    if (animalAEliminar != null)
                    {
                        conexion.Animals.Remove(animalAEliminar);
                        conexion.SaveChanges();

                        // Devuelve true si la eliminación fue exitosa
                        return true;
                    }
                    else
                    {
                        // Devuelve false si el animal no se encontró en la base de datos
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }


        [WebMethod(Description = "Modificar un animal por nombre")]
        public bool ModificarAnimalPorNombre(string nombre, string nuevaEspecie, int nuevaEdad, string nuevoColor, string nuevaUbicacion)
        {
            using (var conexion = new AnimalesEntities())
            {
                try
                {
                    // Busca el animal por su nombre
                    var animalAModificar = conexion.Animals.FirstOrDefault(a => a.Nombre == nombre);

                    // Si se encontró el animal, modifica sus propiedades
                    if (animalAModificar != null)
                    {
                        animalAModificar.Especie = nuevaEspecie;
                        animalAModificar.Edad = nuevaEdad;
                        animalAModificar.Color = nuevoColor;
                        animalAModificar.Ubicacion = nuevaUbicacion;

                        conexion.SaveChanges();

                        // Devuelve true si la modificación fue exitosa
                        return true;
                    }
                    else
                    {
                        // Devuelve false si el animal no se encontró en la base de datos
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }





    }
}
