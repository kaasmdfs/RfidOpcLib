using RfidOpcLib.Unions;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6037")]
    [BinaryEncodingId("ns=3;i=5011")]
    public class RfidScanResult : ScanResult
    {
        public int NoOfSighting { get; set; }
        public int Reserved1 { get; set; }
        public RfidSighting[] Sighting { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteString("CodeType", this.CodeType);
            encoder.WriteDateTime("TimeStamp", this.Timestamp);
            encoder.WriteEncodable<ScanData>("ScanData", this.ScanData);
            encoder.WriteEncodable<Location>("Location", this.Location);
            encoder.WriteEncodableArray<RfidSighting>("Sighting", this.Sighting);
        }

        public override void Decode(IDecoder decoder)
        {
            //This int is needed for some reason to align the rest of the properties
            this.Reserved1 = decoder.ReadInt32("");

            this.CodeType = decoder.ReadString("CodeType");
            this.ScanData = decoder.ReadEncodable<ScanData>("ScanData");
            this.Timestamp = decoder.ReadDateTime("Timestamp");
            //This property doesn't seem to be needed
            // this.NoOfSighting = decoder.ReadInt32("NoOfSighting");

            this.Sighting = decoder.ReadEncodableArray<RfidSighting>("Sighting");
            //We expect a null on this property from this reader
            //this.Location = decoder.ReadEncodable<Location>("LocationSpecified");
        }

        public override string ToString() => $"{{ CodeType={this.CodeType}; Sighting={this.Sighting}; TimeStamp={this.Timestamp}; ScanData={this.ScanData};}}";
    }
}
