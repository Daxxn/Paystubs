using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaystubJsonApp
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : Window
    {
        public bool SaveOnClose { get; private set; } = false;
        public AddView( AddViewModel vm )
        {
            InitializeComponent();
            DataContext = vm;
            InitializeEvents(vm);
        }

        private void InitializeEvents( AddViewModel vm )
        {
            //MainDataEntryGrid.KeyDown += vm.HandleDataEntryKeyDownEvent;
            AddNewPaystubButton.Click += vm.HandleCreateNewPaystub;
            CancelAddButton.Click += CloseEvent;
            SaveAddButton.Click += SaveAndCloseEvent;
        }

        private void CloseEvent( object sender, EventArgs e )
        {
            //OnClosing(new CancelEventArgs());
            Close();
        }

        public void SaveAndCloseEvent( object sender, EventArgs e )
        {
            var vm = DataContext as AddViewModel;
            SaveOnClose = true;
            vm.HandleSave();
            Close();
        }

        private void DeletePaystub( object sender, RoutedEventArgs e )
        {
            var btn = e.Source as Button;
            var data = btn.DataContext as PaystubModel;
            var vm = DataContext as AddViewModel;

            vm.HandlePaystubDelete(data.PaystubID);
        }

        private void Window_KeyDown( object sender, KeyEventArgs e )
        {
            var vm = DataContext as AddViewModel;
            if (e.Key == Key.Enter)
            {
                vm.HandleCreateNewPaystub(sender, e);
            }
            else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.LeftShift)
            {
                vm.ClearEntryValues();
            }
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            var vm = DataContext as AddViewModel;
            if (vm.HasChanged && !SaveOnClose)
            {
                var result = MessageBox.Show(
                    "Paystubs were created. Do you want to get rid of them?",
                    "U sure there bud!?",
                    MessageBoxButton.YesNo
                );

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }

        private void TextBox_GotFocus( object sender, RoutedEventArgs e )
        {
            var textBox = e.Source as TextBox;
            Console.WriteLine($"Selection Changed: {textBox.Text}");
        }

        private void TextBox_GotKeyboardFocus( object sender, KeyboardFocusChangedEventArgs e )
        {
            var textBox = e.Source as TextBox;
            textBox.SelectAll();
        }
    }
}
