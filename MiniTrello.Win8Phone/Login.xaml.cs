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
using RestSharp;
namespace MiniTrello.Win8Phone
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
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

                    }
                }
            });
        }
    }
}