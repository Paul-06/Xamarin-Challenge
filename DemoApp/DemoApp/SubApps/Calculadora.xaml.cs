using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.SubApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calculadora : ContentPage
    {
        // Variables para el estado de la calculadora
        private bool isInputStarted = false; // Variable para verificar si se inició la entrada
        private double primerNumero; // Primer número en la operación
        private double segundoNumero; // Segundo número en la operación
        private string operador; // El operador seleccionado

        public Calculadora()
        {
            InitializeComponent();
        }

        // Función para verificar si una cadena es numérica
        private bool IsNumeric(string str)
        {
            double result;
            return double.TryParse(str, out result);
        }

        // Funcion para verificar si un string es un operador
        private bool IsOperator(string str)
        {
            string[] operationSigns = { "+", "-", "×", "÷" };

            // Verificar si es un signo
            return operationSigns.Contains(str);
        }

        // Manejador de eventos para los botones numéricos y operadores
        private void NumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (IsNumeric(buttonText))
            {
                // Si no se ha iniciado la entrada, reemplazar el 0 con el número presionado
                if (!isInputStarted)
                {
                    ResultEntry.Text = buttonText;
                    isInputStarted = true;
                }
                else
                {
                    // Concatenar el número presionado al contenido actual del ResultEntry
                    ResultEntry.Text += buttonText;
                }
            }
            else if (IsOperator(buttonText))
            {
                // Agregar el 0 a la operación si es el primer botón presionado
                if (!isInputStarted)
                {
                    ResultEntry.Text = "0" + buttonText;
                    isInputStarted = true;
                }
                else
                {
                    // Verificar si el último carácter es un operador antes de concatenar otro
                    if (!IsOperator(ResultEntry.Text.Substring(ResultEntry.Text.Length - 1)))
                    {
                        ResultEntry.Text += buttonText;
                    }
                }
            }
        }

        // Manejadores de eventos para los botones de operadores
        private void btnDividir_Clicked(object sender, EventArgs e)
        {
            this.primerNumero = double.Parse(ResultEntry.Text);
            this.operador = "÷";
            ResultEntry.Text = "";
        }

        private void btnMultiplicar_Clicked(object sender, EventArgs e)
        {
            this.primerNumero = double.Parse(ResultEntry.Text);
            this.operador = "×";
            ResultEntry.Text = "";
        }

        private void btnRestar_Clicked(object sender, EventArgs e)
        {
            this.primerNumero = double.Parse(ResultEntry.Text);
            this.operador = "-";
            ResultEntry.Text = "";
        }

        private void btnSumar_Clicked(object sender, EventArgs e)
        {
            this.primerNumero = double.Parse(ResultEntry.Text);
            this.operador = "+";
            ResultEntry.Text = "";
        }

        private void btnPunto_Clicked(object sender, EventArgs e)
        {
            if (!ResultEntry.Text.Contains("."))
            {
                ResultEntry.Text += ".";
            }
        }

        // Manejador de eventos para el botón "CA" (borrar todo)
        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {
            ResultEntry.Text = "0";
            isInputStarted = false;
        }

        // Manejador de eventos para el botón "DEL" (eliminar último dígito)
        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if (ResultEntry.Text.Length == 1)
            {
                ResultEntry.Text = "0";
                isInputStarted = false;
            }
            else
            {
                ResultEntry.Text = ResultEntry.Text.Substring(0, ResultEntry.Text.Length - 1);
            }
        }

        // Manejador de eventos para el botón "=" (calcular el resultado)
        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            this.segundoNumero = double.Parse(ResultEntry.Text);
            double resultado;

            // Realizar la operación según el operador seleccionado
            switch (this.operador)
            {
                case "+":
                    resultado = this.primerNumero + this.segundoNumero;
                    ResultEntry.Text = resultado.ToString();
                    break;
                case "-":
                    resultado = this.primerNumero - this.segundoNumero;
                    ResultEntry.Text = resultado.ToString();
                    break;
                case "×":
                    resultado = this.primerNumero * this.segundoNumero;
                    ResultEntry.Text = resultado.ToString();
                    break;
                case "÷":
                    resultado = this.primerNumero / this.segundoNumero;
                    ResultEntry.Text = resultado.ToString();
                    break;
            }
        }
    }
}
