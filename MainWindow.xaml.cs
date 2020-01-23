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

namespace ICE3StarterCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
          
            if (rdoLinear.IsChecked == false && rdoDB.IsChecked == false || string.IsNullOrWhiteSpace(txtConvert.Text))
            {
                MessageBox.Show("Neither radio botton was selected or there is no data in the text box to perform the conversion.");
            }

            if (rdoLinear.IsChecked == true)
            {     
                DBToLinear();
            }

            if (rdoDB.IsChecked == true)
            {
                LinearToDB();
            }
        }

        private void LinearToDB()
        {
            double SNRdata = Convert.ToDouble(txtConvert.Text);
            double dB;

            dB = Math.Round(10 * Math.Log10(SNRdata), 4);

            txtConvertOutput.Text = Convert.ToString(dB) + " dB";
        }

        private void DBToLinear()
        {
            double Dbdata = Convert.ToDouble(txtConvert.Text);
            double SNR;
            double divTen;

            divTen = (Dbdata / 10);

            SNR = Math.Round(Math.Pow(10, divTen), 4);

            txtConvertOutput.Text = Convert.ToString(SNR);

        }

        private void btnComputeNoise_Click(object sender, RoutedEventArgs e)
        {

            if (cboTemperatureUnit.Text == "Celsius")
            {
                double temp = Convert.ToDouble(txtTemperature.Text);
                double result = CTOK(temp);
                calcNoiseInDB(result);
            }

            if (cboTemperatureUnit.Text == "Fahrenheit")
            {
                double temp = Convert.ToDouble(txtTemperature.Text);
                double result = FTOK(temp);
                calcNoiseInDB(result);
            }

            if (cboTemperatureUnit.Text == "Kelvin")
            {
                double temp = Convert.ToDouble(txtTemperature.Text);
                calcNoiseInDB(temp);
            }

            
        }

        private double CTOK(double temp)
        {
           double t = Math.Round(273.15 + temp, 4);
            return t;
        }

        private double FTOK(double temp)
        {
            
            double t = Math.Round((temp + 459.67) * 5/9, 4);
            return t;
        }
        private void calcNoiseInDB(double t)
        {
            double k = -228.6;
            double bandWidth = Math.Round(Convert.ToDouble(txtBandwidth.Text) * Math.Pow(10, 6), 4);
            double temperature = Math.Round(t, 4);

            double noise = Math.Round(k + 10 * Math.Log10(temperature) + 10 * Math.Log10(bandWidth), 4);
            txtNoiseOutput.Text = Convert.ToString(noise + " dWB");
        }

        private void rdoLinear_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoDB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtConvert_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBandwidth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
