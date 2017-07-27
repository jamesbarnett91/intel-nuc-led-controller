using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NucLedController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //comboBoxColour.ItemsSource = Enum.GetValues(typeof(LEDController.Colour));
            comboBoxColour.ItemsSource = LEDColour.AvailableColours;
            comboBoxColour.DisplayMemberPath = "DisplayName";
            comboBoxColour.SelectedIndex = 0;
            //comboBoxColour.SelectedValuePath = "ByteValue";
            comboBoxTransition.ItemsSource = LEDTransition.AvailableTransitions;
            comboBoxTransition.DisplayMemberPath = "DisplayName";
            comboBoxTransition.SelectedIndex = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("clicked! " + (LEDColour)comboBoxColour.SelectedItem);
            try
            {
                LEDController.SetLEDState((LEDTransition)comboBoxTransition.SelectedItem, (LEDColour)comboBoxColour.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting LED: {ex.Message.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
