using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace SimpleWin8App
{
    public class MyListener : Listener
    {
        public event EventHandler<MyEventArgs> OnFingers;

        private long _previousFrameTimestamp;

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();

            var delta = frame.Timestamp - _previousFrameTimestamp;
            _previousFrameTimestamp = frame.Timestamp;
            if (delta < 1000) return;

            if (frame.Fingers.Count > 0 && OnFingers != null)
            {
                OnFingers(this, new MyEventArgs(frame.Fingers.Count));
            }
        }
    }
}
