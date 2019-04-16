using GenericCodes.CRUD.WPF.Core;
using GenericCodes.CRUD.WPF.UIServices;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using GenericCodes.CRUD.WPF.Views.CRUD;
using System;
using System.Windows;

namespace AplikasiTog.UIServices
{
    public class DialogService : IDialogService
    {
        public void ShowAddEditWindow(PopupTypeEnum type, PopupViewModelBase viewModel, string windowStyleName)
        {
            try
            {
                Style windowStyle = Application.Current.FindResource(windowStyleName) as Style;
                AddEditPopupWindow popupModernWindow = new AddEditPopupWindow()
                {
                    Title = type.ToString(),
                    DataContext = viewModel,
                    Style = windowStyle,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Application.Current.MainWindow
                };

                viewModel.CloseAssociatedWindow = () =>
                {
                    popupModernWindow.Close();
                };
                popupModernWindow.Closing += (s, e) =>
                {
                    viewModel.Closing();
                };
                popupModernWindow.Loaded += popupModernWindow_Loaded;
                popupModernWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void popupModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;

            window.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            window.Height = window.DesiredSize.Height;
            window.Width = window.DesiredSize.Width;
            //----- Set start-up location dynamic to center of parent window
            Window mainWindow = Application.Current.MainWindow;
            window.Left = mainWindow.Left + (mainWindow.Width - window.ActualWidth) / 2;
            window.Top = mainWindow.Top + (mainWindow.Height - window.ActualHeight) / 2;
        }

        public bool ShowConfirmDialog(string title, string message)
        {
            var messageBoxResult = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            return messageBoxResult == MessageBoxResult.Yes;
        }

        public bool ShowOKDialog(string title, string message)
        {
            var messageBoxResult = MessageBox.Show(message, title, MessageBoxButton.OK);
            return messageBoxResult == MessageBoxResult.OK;
        }



    }

}
