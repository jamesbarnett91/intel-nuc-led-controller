using System;
using System.Linq;
using System.Timers;

namespace NucLedController
{    
    class FadeColourCyclerControlMode : IControlMode
    {
        private enum FadeDirection
        {
            Up,
            Down
        }

        private readonly Timer timer;
        private int currentColourIndex;
        private readonly int maxColourIndex = LEDColour.AvailableColours.Count - 1;

        private readonly byte brightnessStepping = 0x05; 
        private byte currentBrightness = 0x00;
        private byte maxBrightness = 0x64;
        private byte minBrightness = 0x00;
        private FadeDirection fadeDirection = FadeDirection.Up;

        public FadeColourCyclerControlMode(int intervalMs)
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);

            // This should be enforced through the GUI but it causes the sliders input text box to behave weirdly when typing a single number
            if (intervalMs < 100)
            {
                intervalMs = 100;
            }

            // There are 0x64 (100d) brightness values, so divide the desired interval time by brightness stepping to get the tick rate
            timer.Interval = intervalMs / (100/brightnessStepping);
        }

        private void Tick(object source, ElapsedEventArgs e)
        {
            LEDController.SetLEDState(LEDTransition.getLEDTransition("ALWAYS_ON"), LEDColour.AvailableColours.ElementAt(currentColourIndex), currentBrightness);

            // TODO more of the fade duration should be spent at the lower brightness levels, since the perceived change in brightness is much more apparent at the bottom end.
            // e.g. the perceived difference between 0x10 and 0x20 is much larger than 0x50 and 0x60, so scaling up linearly wastes a lot of time increasing imperceivable brightness changes at the top of the range.
            // presumably its logarithmic, like with sound?
            if (fadeDirection == FadeDirection.Up)
            {
                if(currentBrightness == maxBrightness)
                {
                    // Fade up finished, start fading down
                    fadeDirection = FadeDirection.Down;
                }
                else
                {
                    // Continue fading up
                    currentBrightness += brightnessStepping;
                }
            }
            else // Fading down
            {
                if(currentBrightness == minBrightness)
                {
                    // Fade cycle complete. Change colour and fade up
                    fadeDirection = FadeDirection.Up;

                    if (currentColourIndex == maxColourIndex)
                    {
                        currentColourIndex = 0;
                    }
                    else
                    {
                        currentColourIndex++;
                    }
                }
                else
                {
                    // Continue fading down
                    currentBrightness -= brightnessStepping;
                }
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
