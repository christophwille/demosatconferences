using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private const string zumoUrl = "https://waadthefsck.azure-mobile.net/";
        private const string zumoApplicationKey = "REMOVED";

        private const string auth0ClientId = "REMOVED";
        private const string auth0RedirectUri = "http://localhost:8181/callback";
        private const string auth0WaadDomain = "waadthefsck.onmicrosoft.com";

        const string auth0StartUri =
            "https://christophwille.auth0.com/authorize/?client_id={0}&redirect_uri={1}&response_type=token&connection={2}&scope=openid";

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string startUri = String.Format(auth0StartUri, auth0ClientId, auth0RedirectUri, auth0WaadDomain);
                
            var result = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None, 
                new Uri(startUri), 
                new Uri(auth0RedirectUri));

            var responseUri = new Uri(result.ResponseData);
            
            // Remove the leading #, because it is the fragment and not the query (data returned using hash fragment)
            var decoder = new WwwFormUrlDecoder(responseUri.Fragment.Substring(1));

            // Decode using http://openidtest.uninett.no/jwt if you like to see what's in it
            string jwttoken = decoder.GetFirstValueByName("id_token");

            var mobileService = new MobileServiceClient(zumoUrl, zumoApplicationKey)
            {
                CurrentUser = new MobileServiceUser("waaddoyouexpecttosee")
                {
                    MobileServiceAuthenticationToken = jwttoken
                }
            };

            var todoTable = mobileService.GetTable<TodoItem>();

            await todoTable.InsertAsync(new TodoItem()
                                    {
                                        Text = "With greetings from a WAAD user at  " + DateTime.Now.ToString(),
                                        Complete = false
                                    });

            var results = await todoTable
                                    .Where(todoItem => todoItem.Complete == false)
                                    .ToListAsync();

            await new MessageDialog("Done").ShowAsync();
        }
    }
}
