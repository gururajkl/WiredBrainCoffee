using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Controls
{
    [ContentProperty(Name = nameof(Customer))]
    public sealed partial class CustomerDetailsUserControl : UserControl
    {
        private Customer customer;

        public CustomerDetailsUserControl()
        {
            this.InitializeComponent();
        }

        public Customer Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                textBoxFirstname.Text = customer?.Firstname ?? "";
                textBoxLastname.Text = customer?.Lastname ?? "";
                checkBoxIsDeveloper.IsChecked = customer.IsDeveloper;
            }
        }

        private void IsTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCustomer();
        }

        private void IsCheckBoxCheckedOrUnChecked(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();
        }

        public void UpdateCustomer()
        {
            if (Customer != null)
            {
                Customer.Firstname = textBoxFirstname.Text;
                Customer.Lastname = textBoxLastname.Text;
                Customer.IsDeveloper = checkBoxIsDeveloper.IsChecked.GetValueOrDefault();
            }
        }

        public void ClearTextBoxValues()
        {
            textBoxFirstname.Text = string.Empty;
            textBoxLastname.Text = string.Empty;
            checkBoxIsDeveloper.IsChecked = false;
        }
    }
}
