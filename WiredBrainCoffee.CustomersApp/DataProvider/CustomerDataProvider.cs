using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.DataProvider
{
    public class CustomerDataProvider
    {
        private static readonly string customerFileName = "customers.json";
        private static readonly StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public async Task<IEnumerable<Customer>> LoadCustomersAsync()
        {
            var storageFile = await localFolder.TryGetItemAsync(customerFileName) as StorageFile;
            List<Customer> customersList = null;

            if (storageFile == null)
            {
                customersList = new List<Customer>()
                {
                    new Customer() {Firstname = "Gururaj", Lastname = "KL", IsDeveloper = true},
                    new Customer() {Firstname = "Jagadish", Lastname = "TD", IsDeveloper = false},
                    new Customer() {Firstname = "Yash", Lastname = "Yagarwal", IsDeveloper = true},
                    new Customer() {Firstname = "Ram", Lastname = "Shah", IsDeveloper = true},
                    new Customer() {Firstname = "Akshay", Lastname = "S", IsDeveloper = true},
                    new Customer() {Firstname = "Bharath", Lastname = "SS", IsDeveloper = false},
                    new Customer() {Firstname = "Srijan", Lastname = "Hosmane", IsDeveloper = true},
                };
            }
            else
            {
                using (var stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    using (var dataReader = new DataReader(stream))
                    {
                        await dataReader.LoadAsync((uint)stream.Size);
                        var json = dataReader.ReadString((uint)stream.Size);
                        customersList = JsonConvert.DeserializeObject<List<Customer>>(json);
                    }
                }
            }
            return customersList;
        }

        public async Task SaveCustomersAsync(IEnumerable<Customer> customers)
        {
            var storageFile = await localFolder.CreateFileAsync(customerFileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var dataWriter = new DataWriter(stream))
                {
                    var jsonConvert = JsonConvert.SerializeObject(customers, Formatting.Indented);
                    dataWriter.WriteString(jsonConvert);
                    await dataWriter.StoreAsync();
                }
            }
        }
    }
}
