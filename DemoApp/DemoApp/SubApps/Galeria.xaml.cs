using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.SubApps
{
    // La siguiente línea especifica que esta clase se compila con opciones de compilación XAML.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Galeria : ContentPage
    {
        // Se declara una colección observable de elementos de imagen.
        public ObservableCollection<ImageItem> ImageList { get; set; }

        // Constructor de la página Galeria.
        public Galeria()
        {
            InitializeComponent();

            // Se inicializa la colección de imágenes con algunos objetos ImageItem.
            ImageList = new ObservableCollection<ImageItem>
            {
                new ImageItem { ImageSource = "milkyWay.jpeg" },
                new ImageItem { ImageSource = "universe.jpeg" },
                new ImageItem { ImageSource = "naveEspacial.jpeg" },
                new ImageItem { ImageSource = "blackHole.jpeg" },
                new ImageItem { ImageSource = "saturno.jpeg" },
                new ImageItem { ImageSource = "andromeda.jpeg" },
                new ImageItem { ImageSource = "sistemaBinario.jpeg" },
                new ImageItem { ImageSource = "robot.jpeg" },
                // Puedes agregar más imágenes aquí
            };

            // Se establece el contexto de enlace de datos para esta página, lo que permite que los elementos de la interfaz gráfica se enlacen a las propiedades de esta clase.
            BindingContext = this;
        }
    }

    // La siguiente clase representa un elemento de imagen.
    public class ImageItem
    {
        public string ImageSource { get; set; } // Propiedad que almacena la ruta de la imagen.
    }
}
