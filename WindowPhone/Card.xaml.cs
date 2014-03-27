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
    public partial class Card : PhoneApplicationPage
    {
        public Card()
        {
            InitializeComponent();

            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/lanes/" + Convert.ToInt64(App.LaneId) + "/cards/" + App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<CardModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                       
                        CardList.ItemsSource = r.Data;

                    }
                }
            });
        }
    }
}