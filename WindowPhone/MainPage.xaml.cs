﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MiniTrello.Api.Models;
using WindowPhone.Resources;
using RestSharp;
namespace WindowPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            var model = new AccountLoginModel()
            {

                Email = Email.Text,
                Password = Password.Password

            };

            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(model);

            var asyncHandler = client.ExecuteAsync<AuthenticationModel>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {
                        App.Token = r.Data.Token;
                        MessageBox.Show("Welcome");
                        NavigationService.Navigate(new Uri("/organization.xaml", UriKind.Relative));
                    }
                }
            });
        }

        // Código de ejemplo para compilar una ApplicationBar traducida
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}