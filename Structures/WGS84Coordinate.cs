using System;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6212")]
    [BinaryEncodingId("ns=3;i=5015")]
    public class WGS84Coordinate : Structure
    {
        public string N_S_Hemisphere { get; set; }
        public double Latitude { get; set; }
        public string E_W_Hemisphere { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public DateTime Timestamp { get; set; }
        public double DilutionOfPrecision { get; set; }
        public int UsefulPrecisionLatLon { get; set; }
        public int UsefulPrecisionAlt { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteString("N_S_Hemisphere", this.N_S_Hemisphere);
            encoder.WriteDouble("Latitude", this.Latitude);
            encoder.WriteString("E_W_Hemisphere", this.E_W_Hemisphere);
            encoder.WriteDouble("Longitude", this.Longitude);
            encoder.WriteDouble("Altitude", this.Altitude);
            encoder.WriteDateTime("TimeStamp", this.Timestamp);
            encoder.WriteDouble("DilutionOfPrecision", this.DilutionOfPrecision);
            encoder.WriteInt32("UsefulPrecisionLatLon", this.UsefulPrecisionLatLon);
            encoder.WriteInt32("UsefulPrecisionAlt", this.UsefulPrecisionAlt);
        }
        public override void Decode(IDecoder decoder)
        {
            this.N_S_Hemisphere = decoder.ReadString("N_S_Hemisphere");
            this.Latitude = decoder.ReadDouble("Latitude");
            this.E_W_Hemisphere = decoder.ReadString("E_W_Hemisphere");
            this.Longitude = decoder.ReadDouble("Longitude");
            this.Altitude = decoder.ReadDouble("Altitude");
            this.Timestamp = decoder.ReadDateTime("Timestamp");
            this.DilutionOfPrecision = decoder.ReadDouble("DilutionOfPrecision");
            this.UsefulPrecisionLatLon = decoder.ReadInt32("UsefulPrecisionLatLon");
            this.UsefulPrecisionAlt = decoder.ReadInt32("UsefulPrecisionAlt");

        }

        public override string ToString() => $"{{ N_S_Hemisphere={this.N_S_Hemisphere}; Latitude={this.Latitude}; E_W_Hemisphere={this.E_W_Hemisphere}; Longitude={this.Longitude};" +
            $" Altitude={this.Altitude}; Timestamp={this.Timestamp}; DilutionOfPrecision={this.DilutionOfPrecision};" +
            $" UsefulPrecisionLatLon={this.UsefulPrecisionLatLon}; UsefulPrecisionAlt={this.UsefulPrecisionAlt};}}";
    }
}
