using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private int diameter = 10; // Tamaño por defecto
        private Brush brushColor = Brushes.Black; // Color por defecto
        private bool pintando = false;

        public MainWindow()
        {
            InitializeComponent();
            CargarColoresDesdeArchivo("D:/colores.txt"); // Asegúrate de que la ruta sea correcta
        }

        private void CargarColoresDesdeArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                string[] colores = File.ReadAllLines(ruta);
                foreach (string color in colores)
                {
                    AgregarColorRadioButton(color);
                }
            }
            else
            {
                MessageBox.Show("El archivo no fue encontrado.");
            }
        }

        private void AgregarColorRadioButton(string colorNombre)
        {
            RadioButton radioButton = new RadioButton
            {
                Content = colorNombre,
                Tag = colorNombre // Guardar el nombre del color
            };
            radioButton.Checked += ColorRadioButton_Checked; // Asignar el evento de selección
            seleccion_colores.Children.Add(radioButton);
        }

        private void Pintar(Brush brush, Point position)
        {
            Ellipse elipseNueva = new Ellipse
            {
                Fill = brush,
                Width = diameter,
                Height = diameter
            };

            Canvas.SetTop(elipseNueva, position.Y - (diameter / 2)); // Centrar el trazo
            Canvas.SetLeft(elipseNueva, position.X - (diameter / 2)); // Centrar el trazo
            area_dibujo.Children.Add(elipseNueva);
        }

        private void area_dibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintando)
            {
                Point mousePosition = e.GetPosition(area_dibujo);
                Pintar(brushColor, mousePosition);
            }
        }

        private void area_dibujo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pintando = true;
            Point mousePosition = e.GetPosition(area_dibujo);
            Pintar(brushColor, mousePosition);
        }

        private void area_dibujo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pintando = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && int.TryParse(radioButton.Tag.ToString(), out int size))
            {
                diameter = size; // Establece el tamaño según el RadioButton seleccionado
            }
        }

        private void ColorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Convertir el nombre del color a un Brush
                try
                {
                    brushColor = (Brush)new BrushConverter().ConvertFromString(radioButton.Tag.ToString());
                }
                catch
                {
                    MessageBox.Show("Color no válido: " + radioButton.Tag.ToString());
                }
            }
        }
    }
}