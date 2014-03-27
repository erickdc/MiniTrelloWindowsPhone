using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using MiniTrello.Api.Models;
using RestSharp;
namespace WindowPhone
{
    public partial class Organization : PhoneApplicationPage
    {
        public Organization()
        {
            InitializeComponent();
            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/organizations/" + App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<OrganizationModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                        MessageBox.Show("Funciono");
                        OrgaList.ItemsSource = r.Data;
                      

                    }
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            App.OrganizationId = bt.Tag;
            NavigationService.Navigate(new Uri("/board.xaml", UriKind.Relative));
        }     
    }
}