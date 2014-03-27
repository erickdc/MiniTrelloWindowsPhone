using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RestSharp;
using MiniTrello.Api.Models;
namespace MiniTrello.Win8Phone
{
    public partial class Organizations : PhoneApplicationPage
    {
        public Organizations()
        {
            InitializeComponent();
            var client = new RestClient("http://minitrelloapierick.apphb.com");
            var request = new RestRequest("/organizations/"+App.Token, Method.GET);
            request.RequestFormat = DataFormat.Json;


            var asyncHandler = client.ExecuteAsync<List<OrganizationModel>>(request, r =>
            {
                if (r.ResponseStatus == ResponseStatus.Completed)
                {
                    if (r.Data != null)
                    {
                        
                        MessageBox.Show("Funciono");
                        GenerateControls(r.Data);

                    }
                }
            });

            
        }

        public void GenerateControls(List<OrganizationModel> list)
        {
            OrgaList.ItemsSource = list;
            
           
        }

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }
    }
}