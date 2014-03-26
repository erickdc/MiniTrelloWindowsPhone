using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MiniTrello.Api.Models;
using MiniTrello.Win8Phone.Resources;
using RestSharp;
namespace MiniTrello.Win8Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = new AccountRegisterModel()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Email = Email.Text,
                Password = Password.Password,
                ConfirmPassword = ConfirmPassword.Password
            };

            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(model);

            var asyncHandler = client.ExecuteAsync<AccountRegisterModel>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                    }
                }
            });
        }

      

     
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}