using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace maestro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Alternar el estado de selección
                if (button.Background == System.Windows.Media.Brushes.Gray)
                {
                   // button.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(button.Tag.ToString());
                }
                else
                {
                    button.Background = System.Windows.Media.Brushes.Gray; // Marcado
                }
            }
        }

        private void HerramientaButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Alternar el estado de selección
                if (button.Background == System.Windows.Media.Brushes.Gray)
                {
                    button.Background = System.Windows.Media.Brushes.LightBlue; // Color original
                }
                else
                {
                    button.Background = System.Windows.Media.Brushes.Gray; // Marcado
                }
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("selecciones.txt"))
            {
                writer.WriteLine("Colores seleccionados:");
                foreach (Button button in ColoresPanel.Children)
                {
                    if (button.Background == System.Windows.Media.Brushes.Gray)
                    {
                        writer.WriteLine(button.Tag.ToString());
                    }
                }

                writer.WriteLine("Herramientas seleccionadas:");
                foreach (Button button in HerramientasPanel.Children)
                {
                    if (button.Background == System.Windows.Media.Brushes.Gray)
                    {
                        writer.WriteLine(button.Tag.ToString());
                    }
                }
            }

            MessageBox.Show("Selecciones guardadas en 'selecciones.txt'.");
        }
    }
}