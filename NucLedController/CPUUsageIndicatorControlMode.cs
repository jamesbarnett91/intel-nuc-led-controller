using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NucLedController
{
    class CPUUsageIndicatorControlMode : IControlMode
    {

        private readonly Timer timer;
        private readonly PerformanceCounter perfCounter;
        private LEDColour currentColour;

        public CPUUsageIndicatorControlMode(int intervalMs)
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);
            timer.Interval = intervalMs;

            perfCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            currentColour = LEDColour.getLEDColour("WHITE");
        }

        private void Tick(object source, ElapsedEventArgs e)
        {
            float cpuUtilisation = perfCounter.NextValue();

            LEDColour targetColour = mapUsageToColour(cpuUtilisation);
            if(currentColour != targetColour)
            {
                LEDController.SetLEDState(LEDTransition.getLEDTransition("ALWAYS_ON"), targetColour);
                currentColour = targetColour;
            }

        }

        // TODO add option to map usage to brighness
        private LEDColour mapUsageToColour(float utilisation)
        {
            if(utilisation <= 33)
            {
                return LEDColour.getLEDColour("GREEN");
            }
            else if (utilisation > 66)
            {
                return LEDColour.getLEDColour("RED");
            }
            else
            {
                return LEDColour.getLEDColour("YELLOW");
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Start();
        }
    }
}
