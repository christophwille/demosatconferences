using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AqiPhoneApp.Models;
using Microsoft.Phone.Notification;
using Microsoft.WindowsAzure.MobileServices;

namespace AqiPhoneApp
{
    public class AqiClient
    {
        private const string MobileServiceUrl = "https://aqidev.azure-mobile.net/";
        private const string MobileServiceApplicationKey = "REMOVED";

        public AqiClient()
        {
            CreateMobileServiceReference = () => new MobileServiceClient(MobileServiceUrl, MobileServiceApplicationKey);
        }

        public Func<MobileServiceClient> CreateMobileServiceReference { get; set; }

        public async Task<Messpunkt> GetMesspunktAsync(string stationId)
        {
            var mp = await CreateMobileServiceReference()
                .InvokeApiAsync<Messpunkt>("messpunkt", HttpMethod.Get, new Dictionary<string, string>()
                {
                    { "stationId", stationId}
                });

            return mp;
        }

        public async Task SendFeedbackAsync(string text)
        {
            var ms = CreateMobileServiceReference();

            var feedback = new Feedback()
            {
                Text = text
            };

            await ms.GetTable<Feedback>().InsertAsync(feedback)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        private const string PushChannelName = "AqiPushChannel";
        private static HttpNotificationChannel _pushChannel;

        public async Task AcquirePushChannelAsync(string stationId)
        {
            if (null != _pushChannel) return;

            try
            {
                _pushChannel = HttpNotificationChannel.Find(PushChannelName);

                if (_pushChannel == null)
                {
                    _pushChannel = new HttpNotificationChannel(PushChannelName);
                    _pushChannel.Open();
                    _pushChannel.BindToShellTile();
                }

                // ChannelUri can be null, don't forget to check (SIM-less dev phones)
                if (null != _pushChannel.ChannelUri)
                {
                    IMobileServiceTable<StationPush> channelTable =
                        CreateMobileServiceReference().GetTable<StationPush>();

                    var channel = new StationPush() {Uri = _pushChannel.ChannelUri.ToString(), StationId = stationId};

                    await channelTable.InsertAsync(channel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
