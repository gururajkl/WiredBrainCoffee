using Windows.Foundation.Metadata;
using WiredBrainCoffee.CustomersApp.Base;

namespace WiredBrainCoffee.CustomersApp.Model
{
    [CreateFromString(MethodName = "WiredBrainCoffee.CustomersApp.Model.CustomerConverter.CreateCustomerFromString")]
    public class Customer : Observable
    {
        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged();
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged();
            }
        }

        private bool isDeveloper;
        public bool IsDeveloper
        {
            get { return isDeveloper; }
            set
            {
                isDeveloper = value;
                OnPropertyChanged();
            }
        }
    }
}
