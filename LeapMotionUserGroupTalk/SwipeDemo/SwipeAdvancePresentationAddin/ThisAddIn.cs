using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Leap;
using SwipeDemo;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace SwipeAdvancePresentationAddin
{
    //
    // Inspired by: http://leapoffice.codeplex.com/
    //
    public partial class ThisAddIn
    {
        private Controller _controller;
        private HorizontalSwipeListener _listener;
        private DateTime _lastGesture;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _listener = new HorizontalSwipeListener();
            _listener.OnSwipe += ListenerOnOnSwipe;

            _lastGesture = DateTime.Now.AddSeconds(-1);
            _controller = new Controller(_listener);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            _controller.RemoveListener(_listener);
            _controller.Dispose();
        }

        private void ListenerOnOnSwipe(object sender, SwipeEventArgs args)
        {
            if (Application.SlideShowWindows.Count != 1) return;
            if (Application.ActivePresentation.SlideShowWindow == null) return;
            if (DateTime.Now - _lastGesture <= new TimeSpan(0, 0, 2)) return;

            _lastGesture = DateTime.Now;

            if (args.Direction == SwipeDirection.Right)
            {
                Application.ActivePresentation.SlideShowWindow.View.Next();
            }
            else
            {
                Application.ActivePresentation.SlideShowWindow.View.Previous();
            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
