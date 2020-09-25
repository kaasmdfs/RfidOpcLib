using System;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6122")]
    [BinaryEncodingId("ns=3;i=5015")]
    public class LocalCoordinate : Structure
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public DateTime Timestamp { get; set; }
        public double DilutionOfPrecision { get; set; }
        public int UsefulPrecicision { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteDouble("X", this.X);
            encoder.WriteDouble("Y", this.Y);
            encoder.WriteDouble("Z", this.Z);
            encoder.WriteDateTime("Timestamp", this.Timestamp);
            encoder.WriteDouble("DilutionOfPrecision", this.DilutionOfPrecision);
            encoder.WriteInt32("UsefulPrecicision", this.UsefulPrecicision);
        }
        public override void Decode(IDecoder decoder)
        {
            this.X = decoder.ReadDouble("X");
            this.Y = decoder.ReadDouble("Y");
            this.Z = decoder.ReadDouble("Z");
            this.Timestamp = decoder.ReadDateTime("Timestamp");
            this.DilutionOfPrecision = decoder.ReadDouble("DilutionOfPrecision");
            this.UsefulPrecicision = decoder.ReadInt32("UsefulPrecicision");

        }

        public override string ToString() => $"{{ X={this.X}; Y={this.Y}; Z={this.Z}; Timestamp={this.Timestamp}; DilutionOfPrecision={this.DilutionOfPrecision}; UsefulPrecicision={this.UsefulPrecicision};}}";
    }
}
