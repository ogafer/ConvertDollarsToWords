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
        #region Variables
        ConvertToWords convertToWords = new ConvertToWords();
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
            convertToWords.ParsingTheString(txtDollars.Text, out long dollars, out int cents, out string Error);
            if (Error == "")
                txtShowWording.Text = convertToWords.DollarsToWords(dollars, cents);
            else
                MessageBox.Show(Error);
        }
    }
}
