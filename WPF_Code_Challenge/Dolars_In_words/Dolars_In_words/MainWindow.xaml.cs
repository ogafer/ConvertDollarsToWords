using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Dollars_In_words;

namespace Dollars_In_words
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Instance
        Dolars_In_words.ServiceReferenceClient.ServiceConvertToDollarsClient dollarsConversion = new Dolars_In_words.ServiceReferenceClient.ServiceConvertToDollarsClient();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnQuit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_convertToWording(object sender, RoutedEventArgs e)
        {
            long dollars = 0;
            int cents = 0;
            string error = "";
            dollarsConversion.ParsingTheString(txtDollars.Text, ref dollars, ref cents, ref error);
            if (error == "")
            {
                dollarsConversion.DollarsToWords(dollars, cents);
                txtShowWording.Text = dollarsConversion.Result();
            }
            else
                MessageBox.Show(error);
        }
    }
}
