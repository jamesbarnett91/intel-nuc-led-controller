namespace NucLedController
{
    class LEDController
    {

        private static readonly byte POWER_LED_COMMAND_CODE = 0x01;
        private static readonly byte RING_LED_COMMAND_CODE = 0x02;

        public static void SetLEDState(LEDTransition transition, LEDColour colour)
        {
            byte brightness = 0x64; // Just hardcode to max brightness for now
            WMISystemManagement.Instance.WriteData(new byte[] { RING_LED_COMMAND_CODE, brightness, transition.ByteValue, colour.ByteValue });
        }

    }
}
