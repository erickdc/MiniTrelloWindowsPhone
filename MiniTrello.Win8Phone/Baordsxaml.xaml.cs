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
using MiniTrello.Api.Models;
using RestSharp;
namespace MiniTrello.Win8Phone
{
    public partial class Baordsxaml : PhoneApplicationPage
    {
        public Baordsxaml()
        {
            InitializeComponent();
            //string parameter = string.Empty;
            /*long Id = 0;
            if (NavigationContext.QueryString.TryGetValue("parameter", out parameter))
            {
                MessageBox.Show(parameter);
                Id = Convert.ToInt32(parameter);
            }*/
           /* var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("organizations/"+Id+"/boards/" + App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<BoardModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {

                        MessageBox.Show("Funciono");
                        GenerateControls(r.Data);

                    }
                }
            });*/
        }

        public void GenerateControls(List<BoardModel> list)
        {
          //  BoardList.ItemsSource = list;


        }

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }
    }
}