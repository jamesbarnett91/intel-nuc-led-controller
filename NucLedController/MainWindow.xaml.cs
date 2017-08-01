using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NucLedController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IControlMode currentControlMode = null;

        public MainWindow()
        {
            InitializeComponent();
            comboBoxColour.ItemsSource = LEDColour.AvailableColours;
            comboBoxColour.DisplayMemberPath = "DisplayName";
            comboBoxColour.SelectedIndex = 0;
            comboBoxTransition.ItemsSource = LEDTransition.AvailableTransitions;
            comboBoxTransition.DisplayMemberPath = "DisplayName";
            comboBoxTransition.SelectedIndex = 0;

        }

        private void applyChanges(object sender, RoutedEventArgs e)
        {

            try
            {

                // Stop the current control mode if there is one
                if(currentControlMode != null)
                {
                    currentControlMode.Stop();
                }

                TabItem tabItem = tabControl.SelectedItem as TabItem;
                if(tabItem.Name.ToString() == "AUTO")
                {
                    if(radioButtonCycleColour.IsChecked ?? false)
                    {
                        currentControlMode = new BlinkColourCyclerControlMode((int) sliderCycleRate.Value);
                        currentControlMode.Start();
                        lableStatusText.Content = "Colour cycle mode enabled";
                    }
                    else if (radioButtonFadeColour.IsChecked ?? false)
                    {
                        currentControlMode = new FadeColourCyclerControlMode((int) sliderFadeRate.Value);
                        currentControlMode.Start();
                        lableStatusText.Content = "Colour fade mode enabled";
                    }
                }
                else
                {
                    if (radioButtonDisableLed.IsChecked ?? false)
                    {
                        LEDController.DisableLED();
                        lableStatusText.Content = "LED disabled";
                    }
                    else
                    {
                        LEDTransition transition = comboBoxTransition.SelectedItem as LEDTransition;
                        LEDColour colour = comboBoxColour.SelectedItem as LEDColour;
                        LEDController.SetLEDState(transition, colour);
                        lableStatusText.Content = $"LED set: {colour.DisplayName} - {transition.DisplayName}";
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting LED: {ex.Message.ToString()}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

    }
}
