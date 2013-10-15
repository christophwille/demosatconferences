using System;
using System.Net;
using System.Net.Http;
using System.Windows;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MyTestClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            // Use this *ONLY* against the self-signed certificate of IIS Express
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
#endif
        }

        private const string Resource = "https://waadthefsck.onmicrosoft.com/WaadWebApiDemoVs2013"; // ida:Audience of service
        private const string ClientId = "0f68b397-7406-41fc-bad3-adf90d833203"; // Native Application Client ID
        private const string AppId = "http://WaadWebApiDemoClientVs2013"; // Native Application redirect URL

        private async void TestIt_Click(object sender, RoutedEventArgs e)
        {
            var authCtx = new AuthenticationContext("https://login.windows.net/waadthefsck.onmicrosoft.com");
            AuthenticationResult authRes = authCtx.AcquireToken(Resource,ClientId,new Uri(AppId));

            string authHeader = authRes.CreateAuthorizationHeader();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44301/Api/Values");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);

            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);
        }
    }
}
