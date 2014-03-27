using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MiniTrello.Api.Controllers;
using RestSharp;
namespace WindowPhone
{
    public partial class Board : PhoneApplicationPage
    {
        public Board()
        {
            InitializeComponent();
            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/organizations/"+Convert.ToInt64(App.OrganizationId)+"/boards/" + App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<BoardModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                        BoardList.ItemsSource = r.Data;

                    }
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            App.BoardId = bt.Tag;
            NavigationService.Navigate(new Uri("/lane.xaml", UriKind.Relative));
        }     
    }
}