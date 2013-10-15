using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AqiPhoneApp.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AqiPhoneApp.Resources;

namespace AqiPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoadAll_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new AqiClient();
            var svcClient = client.CreateMobileServiceReference();

            var messpunkte = await svcClient.GetTable<Messpunkt>().LoadAllAsync(50);
        }

        private async void InvokeSingleMesspunkt_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new AqiClient();
            var wiesmath = await client.GetMesspunktAsync("03:2101");
        }

        private async void SendFeedback_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new AqiClient();
            await client.SendFeedbackAsync("Yay, this is a great demo!");
        }

        private async void TestPush_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new AqiClient();
            await client.AcquirePushChannelAsync("03:2101");
        }
    }
}