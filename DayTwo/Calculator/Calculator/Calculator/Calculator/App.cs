using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public class TodoItem
    {
        string id;
        string name;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "text")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

            }
        }

        [Version]
        public string Version { get; set; }
    }
    public class App : Application
    {
        Microsoft.WindowsAzure.MobileServices.IMobileServiceTable<TodoItem> todoTable;


        string ApplicationURL = @"https://apchin-todo.azurewebsites.net";

        //ListView todoList = new ListView();

        Label result = new Label { Text = "Result" };
        public App()
        {
            MobileServiceClient client = new MobileServiceClient(
               ApplicationURL);

            todoTable = client.GetTable<TodoItem>();

            x.Text = "Default";
            // Button AddToAzure = new Button
            //{
            //    Text = "Add To Azure!",
            //    Font = Font.SystemFontOfSize(NamedSize.Medium),
            //    BorderWidth = 1,
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center
            //};
            //  AddToAzure.Clicked += AddToAzure_Clicked;

            Button Retrieve = new Button
            {
                Text = "Retrieve!",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            Retrieve.Clicked += Retrieve_Clicked;


            var addToAzure = new Button
            {
                Text = "Add",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };

            addToAzure.Clicked += AddToAzure_Clicked;

            var num1 = new Entry { Placeholder = "Number 1" };
            var num2 = new Entry { Placeholder = "Number 2" };
            
            var add = new Button
            {
                Text = "Add",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };
            var sub = new Button
            {
                Text = "Sub",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };
            var mul = new Button
            {
                Text = "Mul",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };
            var div = new Button
            {
                Text = "Div",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };
            add.Clicked += (sender, args) =>
            {
                var calc = new CalculatorNamespace.Calculator();
                var n1 = int.Parse(num1.Text);
                var n2 = int.Parse(num2.Text);
                result.Text = calc.Add(n1, n2).ToString();
            };
            sub.Clicked += (sender, args) =>
            {
                var calc = new CalculatorNamespace.Calculator();
                var n1 = int.Parse(num1.Text);
                var n2 = int.Parse(num2.Text);
                result.Text = calc.Subtract(n1, n2).ToString();
            };
            mul.Clicked += (sender, args) =>
            {
                var calc = new CalculatorNamespace.Calculator();
                var n1 = int.Parse(num1.Text);
                var n2 = int.Parse(num2.Text);
                result.Text = calc.Multiply(n1, n2).ToString();
            };
            div.Clicked += (sender, args) =>
            {
                var calc = new CalculatorNamespace.Calculator();
                var n1 = int.Parse(num1.Text);
                var n2 = int.Parse(num2.Text);
              

                   result.Text = calc.Divide(n1, n2).ToString();
                };
                var sl1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { add, sub }
            };
            var sl2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { mul, div }
            };

           

            MainPage = new ContentPage
            {
                Title = "Calculator",
                Content = new StackLayout
                {
                    Padding = new Thickness(15),
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        num1, num2, sl1, sl2, result
                    }
                }
            };

            //  MainPage = new GridDemoPage();
            var profilePage = new ContentPage
            {
                Title = "Profile",
                Icon = "Profile.png",
                Content = new StackLayout
                {
                    Spacing = 20,
                    Padding = 50,
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        //new Entry { Placeholder = "Username" },
                        //new Entry { Placeholder = "Password", IsPassword = true },
                        //new Button {
                        //    Text = "Login",
                        //    TextColor = Color.White,
                        //    BackgroundColor = Color.FromHex("77D065") },
                        //    AddToAzure,
                        //    Retrieve,

                        num1,
                        num2,
                        add,
                        sub,
                        mul,div,
                        x,                  
                        addToAzure


                    }
                }
            };

            MainPage = profilePage;

            // The root page of your application

          

        }

        Entry x = new Entry { Placeholder = "MyToDo List values"};

        private async void AddToAzure_Clicked(object sender, EventArgs e)
        {

            var todo = new TodoItem { Name = result.Text + "  " + x.Text };
            await AddItem(todo);


        }
        private async Task AddItem(TodoItem todo)
        {
            await SaveTaskAsync(todo);
           //          //
          
        }
        public async Task SaveTaskAsync(TodoItem item)
        {
            if (item.Id == null)
            {
                await todoTable.InsertAsync(item);

            }
            else
            {
                await todoTable.UpdateAsync(item);
            }
        }

        private void Retrieve_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new RetrievePage());
        }

        //private async void AddToAzure_Clicked(object sender, EventArgs e)
        //{

        //    var todo = new TodoItem { Name = result.Text + "Something else"};
        //    await AddItem(todo);


        //}

        //private async Task AddItem(TodoItem todo)
        //{
        //    await SaveTaskAsync(todo);

        //}

        
     
        //public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync(bool syncItems = false)
        //{
        //    try
        //    {
        //        IEnumerable<TodoItem> items = await todoTable
        //            //.Where(todoItem => !todoItem.Done)
                  
        //            .ToEnumerableAsync();
                

        //        return new ObservableCollection<TodoItem>(items);
        //    }
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"Sync error: {0}", e.Message);
        //    }
        //    return null;

        //}
        //public async Task SaveTaskAsync(TodoItem item)
        //{
        //    if (item.Id == null)
        //    {
        //        await todoTable.InsertAsync(item);
               
        //    }
        //    else
        //    {
        //        await todoTable.UpdateAsync(item);
        //    }
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
