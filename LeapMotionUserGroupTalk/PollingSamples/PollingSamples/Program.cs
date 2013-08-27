using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;

namespace PollingSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var controller = new Controller())
            {
                // Calling controller.IsConnected immediately fails (well, other than in console apps you wouldn't access it immediately anyways)
                Thread.Sleep(1000);

                if (controller.IsConnected)
                {
                    Frame frame = controller.Frame();
                    var frameId = frame.Id; // in polling used for checking "Did I see that frame already?"

                    HandList hands = frame.Hands;
                    PointableList pointables = frame.Pointables;
                    FingerList fingers = frame.Fingers;
                    ToolList tools = frame.Tools;

                    Console.WriteLine("# of fingers " + fingers.Count);

                    if (fingers.Count > 0)
                    {
                        Finger farLeft = frame.Fingers.Leftmost;

                        float x = farLeft.TipPosition.x;
                        float y = farLeft.TipPosition.y;
                        float z = farLeft.TipPosition.z;

                        Console.WriteLine("x: {0} y: {1} z: {2}", x, y, z);
                    }
                }
                else
                {
                    Console.WriteLine("No controller detected");
                }

                Console.ReadLine();
            }
        }
    }
}
