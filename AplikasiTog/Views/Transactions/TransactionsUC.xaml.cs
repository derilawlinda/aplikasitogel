using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace AplikasiTog.Views.Transactions
{
    /// <summary>
    /// Interaction logic for TransactionsUC.xaml
    /// </summary>
    public partial class TransactionsUC : UserControl
    {
        public TransactionsUC()
        {
            InitializeComponent();
        }

        private void HandleMethod(object sender, RoutedEventArgs e)
        {

        }

        private void OnNormalChecked(object sender, RoutedEventArgs e)
        {

            this.AmountTextBox.Visibility = Visibility.Visible;
            this.NumberTextBox.MaxLength = 4;
            BBGrid.Visibility = Visibility.Hidden;

        }

        private void OnBBChecked(object sender, RoutedEventArgs e)
        {
            BBGrid.Visibility = Visibility.Visible;
            AmountTextBox.Visibility = Visibility.Hidden;
            this.NumberTextBox.MaxLength = 0;
        }

        private void AmountTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            NormalRadioButton.IsChecked = true;
        }

    }
}
