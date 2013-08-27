using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace SwipeDemo
{
    class Program
    {
        static void Main()
        {
            using (var controller = new Controller())
            {
                // var listener = new SimpleSwipeListener();

                var listener = new HorizontalSwipeListener();
                listener.OnSwipe += (sender, args) => Console.WriteLine(args.Direction);

                controller.AddListener(listener);

                Console.WriteLine("Press Enter to quit...");
                Console.ReadLine();

                controller.RemoveListener(listener);
            }
        }
    }
}
