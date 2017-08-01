namespace NucLedController
{
    class LEDController
    {

        private static readonly byte POWER_LED_COMMAND_CODE = 0x01;
        private static readonly byte RING_LED_COMMAND_CODE = 0x02;

        public static void SetLEDState(LEDTransition transition, LEDColour colour)
        {
            byte brightness = 0x64; // Max brightness value
            WMISystemManagement.Instance.WriteData(new byte[] { RING_LED_COMMAND_CODE, brightness, transition.ByteValue, colour.ByteValue });
        }

        public static void SetLEDState(LEDTransition transition, LEDColour colour, byte brightness)
        {
            WMISystemManagement.Instance.WriteData(new byte[] { RING_LED_COMMAND_CODE, brightness, transition.ByteValue, colour.ByteValue });
        }

        public static void DisableLED()
        {
            WMISystemManagement.Instance.WriteData(new byte[] { RING_LED_COMMAND_CODE, 0x00, 0x00, 0x00 });
        }

    }
}
