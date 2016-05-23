using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Calculator
{
    public partial class RetrievePage : ContentPage
    {
       Microsoft.WindowsAzure.MobileServices.IMobileServiceTable<TodoItem> todoTable;

        public RetrievePage()
        {
            InitializeComponent();
            getitems();
           
            
        }

        public async void getitems()
        {
            string ApplicationURL = @"https://apchin-todo.azurewebsites.net";
            MobileServiceClient client = new MobileServiceClient(
              ApplicationURL);

            todoTable = client.GetTable<TodoItem>();
            ListViewBox.ItemsSource = await GetTodoItemsAsync();
        }

        public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
        {
            try
            {
                IEnumerable<TodoItem> items = await todoTable
                   .ToEnumerableAsync();


                return new ObservableCollection<TodoItem>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;

        }


    }
}
