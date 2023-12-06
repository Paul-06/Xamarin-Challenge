using DemoApp.Models; // Importa el espacio de nombres para acceder a la clase NoteItem
using SQLite; // Importa el espacio de nombres necesario para trabajar con SQLite
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data
{
    public class DatabaseContext
    {
        public SQLiteAsyncConnection Connection { get; set; } // Propiedad para la conexión a la base de datos SQLite

        public DatabaseContext(string path)
        {
            Connection = new SQLiteAsyncConnection(path); // Inicializa una conexión SQLiteAsyncConnection con la ruta especificada
            Connection.CreateTableAsync<NoteItem>().Wait(); // Crea la tabla de notas en la base de datos si no existe
        }

        // Agregar nota (INSERT)
        public async Task<int> InsertItemAsync(NoteItem item)
        {
            return await Connection.InsertAsync(item); // Inserta un objeto NoteItem en la tabla de notas y devuelve el número de filas afectadas (normalmente 1)
        }

        // Mostrar notas (SELECT)
        public async Task<List<NoteItem>> GetItemAsync()
        {
            return await Connection.Table<NoteItem>().ToListAsync(); // Recupera todas las notas de la tabla y las devuelve como una lista de objetos NoteItem
        }

        // Borrar nota (DELETE)
        public async Task<int> DeleteItemAsync(NoteItem item)
        {
            return await Connection.DeleteAsync(item); // Borra un objeto NoteItem de la tabla de notas y devuelve el número de filas afectadas (normalmente 1)
        }
    }
}
