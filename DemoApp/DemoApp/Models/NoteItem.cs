using SQLite; // Importa el espacio de nombres necesario para trabajar con SQLite
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Models
{
    public class NoteItem
    {
        [PrimaryKey, AutoIncrement] // Atributos para la configuración de la tabla SQLite
        public int Id { get; set; } // Propiedad para el identificador único de la nota
        public string Description { get; set; } // Propiedad para el contenido/descripción de la nota
    }
}
