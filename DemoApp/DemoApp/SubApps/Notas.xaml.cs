using DemoApp.Models; // Importa el espacio de nombres para acceder a la clase NoteItem
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.SubApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // Compilación XAML
    public partial class Notas : ContentPage
    {
        public Notas()
        {
            InitializeComponent();
        }

        // Este método se llama cuando la página está a punto de aparecer en la pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems(); // Llama al método para cargar elementos (notas) cuando la página aparece
        }

        // Método para cargar las notas desde la base de datos y mostrarlas en la lista
        private async void LoadItems()
        {
            var items = await App.Context.GetItemAsync(); // Obtiene las notas desde la base de datos utilizando el contexto de la base de datos
            NotesList.ItemsSource = items; // Asigna la lista de notas como origen de datos para el ListView llamado "NotesList"
        }

        // Este método se llama cuando se hace clic en el botón de agregar nota
        private async void OnAgregarNotaClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NuevaNotaEntry.Text))
                {
                    await DisplayAlert("Advertencia", "Debe escribir una nota antes de guardarla.", "Aceptar");
                    return; // Si el campo de texto está vacío, muestra una alerta y sale del método
                }

                // Crea un nuevo objeto NoteItem con la descripción ingresada por el usuario
                var item = new NoteItem
                {
                    Description = NuevaNotaEntry.Text
                };

                // Inserta la nueva nota en la base de datos y obtiene el resultado
                var result = await App.Context.InsertItemAsync(item);

                if (result == 1)
                {
                    NuevaNotaEntry.Text = ""; // Limpia el campo de texto después de guardar la nota
                    LoadItems(); // Vuelve a cargar la lista de notas para mostrar la nueva nota
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la nota.", "Aceptar"); // Muestra una alerta en caso de error
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar"); // Muestra una alerta en caso de una excepción no controlada
            }
        }

        // Este método se llama cuando se hace clic en el botón de eliminar en una celda de la lista
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmación", "¿Está seguro de borrar el elemento?", "Sí", "No"))
            {
                // Obtiene el elemento (nota) que se va a eliminar a partir de los parámetros del evento
                var item = (NoteItem)(sender as MenuItem).CommandParameter;

                // Elimina la nota de la base de datos y obtiene el resultado
                var result = await App.Context.DeleteItemAsync(item);

                if (result == 1)
                {
                    LoadItems(); // Vuelve a cargar la lista de notas para reflejar el cambio
                }
            }
        }
    }
}
