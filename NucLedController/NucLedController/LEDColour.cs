using System.Collections.Immutable;

namespace NucLedController
{
    sealed class LEDColour
    {

        public static readonly ImmutableList<LEDColour> AvailableColours = ImmutableList.Create(
            new LEDColour("CYAN", "Cyan", 0x01),
            new LEDColour("PINK", "Pink", 0x02),
            new LEDColour("YELLOW", "Yellow", 0x03),
            new LEDColour("BLUE", "Blue", 0x04),
            new LEDColour("RED", "Red", 0x05),
            new LEDColour("GREEN", "Green", 0x06),
            new LEDColour("WHITE", "White", 0x07)
        );

        public string Identifier { get; }
        public string DisplayName { get; }
        public byte ByteValue { get; }

        private LEDColour(string identifier, string displayName, byte byteValue)
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
