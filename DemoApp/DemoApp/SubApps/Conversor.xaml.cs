using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp.SubApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Conversor : ContentPage
    {
        // Diccionario que almacena todas las funciones de conversión posibles
        private Dictionary<string, Dictionary<string, Func<double, double>>> conversionFunctions = new Dictionary<string, Dictionary<string, Func<double, double>>>
        {
            {
                "Celsius", new Dictionary<string, Func<double, double>>
                {
                    { "Celsius", value => value }, // No se realiza conversión (Celsius a Celsius)
                    { "Fahrenheit", value => (value * 9/5) + 32 }, // Celsius a Fahrenheit
                    { "Kelvin", value => value + 273.15 }, // Celsius a Kelvin
                }
            },
            {
                "Fahrenheit", new Dictionary<string, Func<double, double>>
                {
                    { "Celsius", value => (value - 32) * 5/9 }, // Fahrenheit a Celsius
                    { "Fahrenheit", value => value }, // No se realiza conversión (Fahrenheit a Fahrenheit)
                    { "Kelvin", value => ((value - 32) * 5/9) + 273.15 }, // Fahrenheit a Kelvin
                }
            },
            {
                "Kelvin", new Dictionary<string, Func<double, double>>
                {
                    { "Celsius", value => value - 273.15 }, // Kelvin a Celsius
                    { "Fahrenheit", value => ((value - 273.15) * 9/5) + 32 }, // Kelvin a Fahrenheit
                    { "Kelvin", value => value }, // No se realiza conversión (Kelvin a Kelvin)
                }
            }
        };

        public Conversor()
        {
            InitializeComponent();
        }

        private void ConvertButton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(inputValueEntry.Text, out double inputValue))
            {
                string fromUnit = fromUnitPicker.SelectedItem.ToString(); // Unidad de origen seleccionada por el usuario
                string toUnit = toUnitPicker.SelectedItem.ToString(); // Unidad de destino seleccionada por el usuario

                double result = Convert(inputValue, fromUnit, toUnit); // Realizar la conversión

                resultLabel.Text = result.ToString(); // Mostrar el resultado en la etiqueta de resultado
            }
            else
            {
                resultLabel.Text = "Ingrese un valor válido."; // Mensaje de error si la entrada no es válida
            }
        }

        private double Convert(double inputValue, string fromUnit, string toUnit)
        {
            double convertedValue = inputValue;

            // Verificar si existen funciones de conversión para las unidades seleccionadas
            if (conversionFunctions.ContainsKey(fromUnit) && conversionFunctions[fromUnit].ContainsKey(toUnit))
            {
                // Obtener la función de conversión correcta del diccionario anidado
                Func<double, double> conversionFunction = conversionFunctions[fromUnit][toUnit];

                // Aplicar la función de conversión para obtener el resultado
                convertedValue = conversionFunction(inputValue);
            }

            return convertedValue; // Devolver el valor convertido
        }
    }
}
