using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Leap;
using SimpleWin8App.Common;

namespace SimpleWin8App
{
    public sealed partial class MainPage : LayoutAwarePage
    {
        private Controller _controller;
        private MyListener _listener;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _listener = new MyListener();

            _listener.OnFingers += ListenerOnOnFingers;
            _controller = new Controller();
            _controller.AddListener(_listener);
        }

        private void ListenerOnOnFingers(object sender, MyEventArgs myEventArgs)
        {
            // This callback happens on a worker thread (intentionally) to show the behavior
            // If you don't marshal to the UI thread, you will get the following exception:
            //  "The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))"

            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            SimpleOutput.Text = myEventArgs.FingerCount.ToString();
                        });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _controller.RemoveListener(_listener);
            _controller.Dispose();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            Debug.WriteLine("LoadState");
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            Debug.WriteLine("SaveState");
        }
    }
}
