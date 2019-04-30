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

namespace AplikasiTog.Views.Nomors
{
    /// <summary>
    /// Interaction logic for NomorUC.xaml
    /// </summary>
    public partial class NomorUC : UserControl
    {
        public NomorUC()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NomorHariIni.IsLoaded)
            {
                if (NomorHariIni.Text.Length == 4)
                {
                    SubmitButton.IsEnabled = true;
                }
                else
                {
                    SubmitButton.IsEnabled = false;
                }
            }

            
        }
    }
}
