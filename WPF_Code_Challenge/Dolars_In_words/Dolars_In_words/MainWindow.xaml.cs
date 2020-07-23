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
            Regex letters = new Regex(@"([\p{L}\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (letters.IsMatch(txtDollars.Text))
                MessageBox.Show("You have a letter... Try again!");
            else
            {
                if (string.IsNullOrEmpty(txtDollars.Text))
                    MessageBox.Show("Write a number!");
                else
                {
                    if (txtDollars.Text.Contains(","))
                    {
                        string[] dollarsAndCents = txtDollars.Text.Split(',');

                        string dollars = dollarsAndCents[0];
                        string cents = dollarsAndCents[1].PadRight(2, '0');
                        txtShowWording.Text = convertToWords.DollarsToWords(Int64.Parse(dollars), Int32.Parse(cents));
                    }
                    else
                    {
                        try
                        {
                            txtShowWording.Text = convertToWords.DollarsToWords(Int64.Parse(txtDollars.Text));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unknow character! Try again!");
                        }
                    }
                }
            }
        }
    }
}
