using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace WaadThreeLeggedDemoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const string Resource = "https://graph.windows.net";
        private const string ClientId = "5c40396b-b8c7-4b38-9647-ac1a01f46b01";
        private const string AppId = "http://WaadThreeLeggedDemoApp";

        private async void DoDemo_Click(object sender, RoutedEventArgs e)
        {
            var authCtx = new AuthenticationContext("https://login.windows.net/waadthefsck.onmicrosoft.com");
            AuthenticationResult authRes = authCtx.AcquireToken(Resource, ClientId, new Uri(AppId));

            // Note: The external library has been modified to use AuthenticationResult, 
            // the old AADJwtToken was removed (plus dependent [extension] classes)
            var ds = new DirectoryDataService("waadthefsck.onmicrosoft.com", authRes);

            var user = ds.directoryObjects.OfType<User>()
                .Where(it => (it.userPrincipalName == "admin@waadthefsck.onmicrosoft.com"))
                .SingleOrDefault();

            MessageBox.Show(user.displayName);
        }
    }
}
