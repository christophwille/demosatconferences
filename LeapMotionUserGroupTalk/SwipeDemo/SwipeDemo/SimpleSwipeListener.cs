using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace SwipeDemo
{
    // Based on sample.cs from SDK
    class SimpleSwipeListener : Listener
    {
        private Object thisLock = new Object();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnConnect(Controller controller)
        {
            controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        public override void OnFrame(Controller controller)
        {
            Frame frame = controller.Frame();
            GestureList gestures = frame.Gestures();

            foreach (var g in gestures)
            {
                if (g.Type == Gesture.GestureType.TYPESWIPE)
                {
                    var swipe = new SwipeGesture(g);
                    SafeWriteLine("Swipe id: " + swipe.Id
                                   + ", " + swipe.State
                                   + ", position: " + swipe.Position
                                   + ", direction: " + swipe.Direction
                                   + ", speed: " + swipe.Speed);
                }
            }
        }
    }
}
