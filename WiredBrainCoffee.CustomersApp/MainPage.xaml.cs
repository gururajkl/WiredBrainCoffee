using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Customer added!");
            await messageDialog.ShowAsync();
        }

        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            // Changing the side and the symbol value.
            int oldColumnValue = Grid.GetColumn(customerList);
            int newColumnValue = oldColumnValue == 0 ? 2 : 0;
            Grid.SetColumn(customerList, newColumnValue);
            moveIconSymbol.Symbol = newColumnValue == 2 ? Symbol.Back : Symbol.Forward;

            // Changing the tooltip value
            Button moveForwardButton = sender as Button;
            if (newColumnValue == 2) ToolTipService.SetToolTip(moveForwardButton, "Move to left");
            else ToolTipService.SetToolTip(moveForwardButton, "Move to right");
        }
    }
}
