using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.SubApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cronometro : ContentPage
    {
        private TimeSpan tiempoTranscurrido; // Variable para mantener el tiempo transcurrido
        private Timer timer; // Temporizador para actualizar el cronómetro
        private bool cronometroEnEjecucion; // Bandera para verificar si el cronómetro está en ejecución

        public Cronometro()
        {
            InitializeComponent();
            tiempoTranscurrido = TimeSpan.Zero; // Inicializa el tiempo transcurrido como cero
            cronometroEnEjecucion = false; // Inicializa la bandera como falso, el cronómetro no está en ejecución
            timer = new Timer(1000); // Crea un temporizador que dispara eventos cada 1000 ms (1 segundo)
            timer.Elapsed += ActualizarCronometro; // Asocia el manejador de eventos ActualizarCronometro al evento Elapsed del temporizador
        }

        // Método para actualizar el cronómetro
        private void ActualizarCronometro(object sender, ElapsedEventArgs e)
        {
            tiempoTranscurrido = tiempoTranscurrido.Add(TimeSpan.FromSeconds(1)); // Incrementa el tiempo transcurrido en 1 segundo

            // Actualiza la etiqueta de cronómetro en la interfaz de usuario (UI) en el hilo principal
            Device.BeginInvokeOnMainThread(() =>
            {
                cronometroLabel.Text = tiempoTranscurrido.ToString(@"hh\:mm\:ss");
            });
        }

        // Manejador de eventos para el botón de Iniciar/Detener
        private void OnIniciarDetenerClicked(object sender, EventArgs e)
        {
            if (cronometroEnEjecucion)
            {
                timer.Stop(); // Detiene el temporizador
                cronometroEnEjecucion = false; // Cambia la bandera a falso, el cronómetro se detiene
                iniciarDetenerButton.Text = "Reanudar"; // Cambia el texto del botón a "Iniciar"
            }
            else
            {
                timer.Start(); // Inicia el temporizador
                cronometroEnEjecucion = true; // Cambia la bandera a verdadero, el cronómetro está en ejecución
                iniciarDetenerButton.Text = "Detener"; // Cambia el texto del botón a "Detener"
            }
        }

        // Manejador de eventos para el botón de Reiniciar
        private void OnResetClicked(object sender, EventArgs e)
        {
            timer.Stop(); // Detiene el temporizador
            tiempoTranscurrido = TimeSpan.Zero; // Reinicia el tiempo transcurrido a cero
            cronometroLabel.Text = "00:00:00"; // Actualiza la etiqueta de cronómetro en la UI
            cronometroEnEjecucion = false; // Cambia la bandera a falso, el cronómetro se detiene
            iniciarDetenerButton.Text = "Iniciar"; // Cambia el texto del botón a "Iniciar"
        }
    }
}
