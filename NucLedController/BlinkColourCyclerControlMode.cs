using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NucLedController
{
    class BlinkColourCyclerControlMode : IControlMode
    {

        private readonly Timer timer;
        private int currentColourIndex;
        private readonly int maxColourIndex = LEDColour.AvailableColours.Count - 1;
        
        public BlinkColourCyclerControlMode(int intervalMs)
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);
            timer.Interval = intervalMs;
        }

        private void Tick(object source, ElapsedEventArgs e)
        {
            LEDController.SetLEDState(LEDTransition.getLEDTransition("ALWAYS_ON"), LEDColour.AvailableColours.ElementAt(currentColourIndex));
            if (currentColourIndex == maxColourIndex)
            {
                currentColourIndex = 0;
            }
            else
            {
                currentColourIndex++;
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
