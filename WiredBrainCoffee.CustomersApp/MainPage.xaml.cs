using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.DataProvider;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        private CustomerDataProvider customerDataProvider;

        public MainPage()
        {
            this.InitializeComponent();
            customerDataProvider = new CustomerDataProvider();
            this.Loaded += MainPage_Loaded;
            App.Current.Suspending += Current_Suspending;
            RequestedTheme = App.Current.RequestedTheme == ApplicationTheme.Dark
                ? ElementTheme.Dark
                : ElementTheme.Light;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            customerListView.Items.Clear();
            var customersFromFile = await customerDataProvider.LoadCustomersAsync();
            foreach (var customer in customersFromFile)
            {
                customerListView.Items.Add(customer);
            }
        }

        private async void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await customerDataProvider.SaveCustomersAsync(customerListView.Items.OfType<Customer>());
            deferral.Complete();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer { Firstname = "New" };
            customerListView.Items.Add(customer);
            customerListView.SelectedItem = customer;
        }

        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = customerListView.SelectedItem as Customer;
            customerListView.Items.Remove(customer);
            customerDetailsControl.ClearTextBoxValues();
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

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (customerListView.SelectedValue as Customer);
            if (customer != null)
            {
                customerDetailsControl.Customer = customer;
            }
        }

        private void ButtonToggleTheme(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = RequestedTheme == ElementTheme.Dark ? ElementTheme.Light
                : ElementTheme.Dark;
        }
    }
}
