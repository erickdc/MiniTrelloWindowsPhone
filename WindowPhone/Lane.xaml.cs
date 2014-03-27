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
    public partial class Lane : PhoneApplicationPage
    {
        public Lane()
        {
            InitializeComponent();
            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/boards/" + Convert.ToInt64(App.BoardId) + "/lanes/" + App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<LaneModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                        
                        LaneList.ItemsSource = r.Data;

                    }
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            App.LaneId = bt.Tag;
            NavigationService.Navigate(new Uri("/card.xaml", UriKind.Relative));

        }     
    }
}