using System.Collections.Immutable;

namespace NucLedController
{
    sealed class LEDTransition
    {

        public static readonly ImmutableList<LEDTransition> AvailableTransitions = ImmutableList.Create(
            new LEDTransition("ALWAYS_ON", "Always on", 0x04),
            new LEDTransition("BLINK_1_HZ", "Blink at 1hz", 0x01),
            new LEDTransition("BLNIK_0.5_HZ", "Blink at 0.5hz", 0x05),
            new LEDTransition("BLINK_0.25_HZ", "Blink at 0.25hz", 0x02),
            new LEDTransition("FADE_1_HZ", "Fade at 1hz", 0x03),
            new LEDTransition("FADE_0.5_HZ", "Fade at 0.5hz", 0x07),
            new LEDTransition("FADE_0.25_HZ", "Fade at 0.25hz", 0x06)
        );

        public string Identifier { get; }
        public string DisplayName { get; }
        public byte ByteValue { get; }

        private LEDTransition(string identifier, string displayName, byte byteValue)
        {
            Identifier = identifier;
            DisplayName = displayName;
            ByteValue = byteValue;
        }

        public override string ToString()
        {
            return this.Identifier + this.ByteValue;
        }

    }
}
