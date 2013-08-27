using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace SwipeDemo
{
    public enum SwipeDirection
    {
        Left,
        Right
    }

    public class SwipeEventArgs : EventArgs
    {
        public SwipeEventArgs(SwipeDirection d)
        {
            Direction = d;
        }

        public SwipeDirection Direction { get; set; }
    }

    class HorizontalSwipeListener : Listener
    {
        public HorizontalSwipeListener()
        {
            Sensitivity = 100;
        }

        public float Sensitivity { get; set; }
        public event EventHandler<SwipeEventArgs> OnSwipe; 

        public override void OnConnect(Controller controller)
        {
            controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();

            foreach (var g in frame.Gestures())
            {
                if (g.Type == Gesture.GestureType.TYPESWIPE && g.State == Gesture.GestureState.STATESTOP)
                {
                    var swipe = new SwipeGesture(g);

                    Vector start = swipe.StartPosition;
                    Vector current = swipe.Position;

                    var delta = start.x - current.x;

                    if (Math.Abs(delta) >= Sensitivity)
                    {
                        var direction = delta < 0 ? SwipeDirection.Right : SwipeDirection.Left;
                        if (null != OnSwipe)
                            OnSwipe(this, new SwipeEventArgs(direction));
                    }
                }
            }
        }
    }
}
