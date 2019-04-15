﻿using Fluent;
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

namespace AplikasiTog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.contentControl.Content = new Views.Transactions.TransactionsUC();
        }

        private void User_Button_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Views.Users.UserUC();
        }

        private void Bet_Button_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Views.Transactions.TransactionsUC();
        }

        private void HandleMethod(object sender, RoutedEventArgs e)
        {
            
        }
    }
}