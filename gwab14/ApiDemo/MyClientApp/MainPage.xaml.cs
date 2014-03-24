using System;
using System.Net.Http;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Filters;
using WindowsRuntime.HttpClientFilters;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MyClientApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private const string Tenant = "extensiondemo.onmicrosoft.com";
        private const string Resource = "https://extensiondemo.onmicrosoft.com/MyApi";
        private const string NativeAppClientId = "";
        private const string NativeAppRedirectUri = "http://anyredirecturl";

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var authCtx = new AuthenticationContext("https://login.windows.net/" + Tenant);
            AuthenticationResult authRes = await authCtx.AcquireTokenAsync(Resource, 
                NativeAppClientId, 
                new Uri(NativeAppRedirectUri));

            string authHeader = authRes.CreateAuthorizationHeader();

            // This is for ignoring the invalid SSL certificate on localhost
            var filter = new HttpBaseProtocolFilter(); 
            filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            var client = new HttpClient(new WinRtHttpClientHandler(filter));

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44310/Api/Values");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);

            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            MessageDialog dlg = new MessageDialog(responseString); 
            await dlg.ShowAsync();
        }
    }
}
