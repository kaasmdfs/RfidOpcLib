using System;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6025")]
    [BinaryEncodingId("ns=3;i=5009")]
    public class RfidSighting : Structure
    {
        public int Antenna { get; set; }
        public int Strength { get; set; }
        public DateTime Timestamp { get; set; }
        public int CurrentPowerLevel { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteInt32("Antenna", this.Antenna);
            encoder.WriteInt32("Strength", this.Strength);
            encoder.WriteDateTime("Timestamp", this.Timestamp);
            encoder.WriteInt32("CurrentPowerLevel", this.CurrentPowerLevel);
        }

        public override void Decode(IDecoder decoder)
        {
            this.Antenna = decoder.ReadInt32("Antenna");
            this.Strength = decoder.ReadInt32("Strength");
            this.Timestamp = decoder.ReadDateTime("Timestamp");
            this.CurrentPowerLevel = decoder.ReadInt32("CurrentPowerLevel");
        }

        public override string ToString() => $"{{ Antenna={this.Antenna}; Strength={this.Strength}; Timestamp={this.Timestamp}; CurrentPowerLevel={this.CurrentPowerLevel};}}";
    }
}
