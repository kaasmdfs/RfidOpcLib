using RfidOpcLib.Unions;
using System;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6020")]
    [BinaryEncodingId("ns=3;i=5002")]
    public class ScanResult :Structure
    {
        public string CodeType { get; set; }
        public ScanData ScanData { get; set; }
        public DateTime Timestamp { get; set; }
        public Location Location { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteString("CodeType", this.CodeType);
            encoder.WriteDateTime("TimeStamp", this.Timestamp);
            encoder.WriteExtensionObject("ScanData", this.ScanData);
            encoder.WriteExtensionObject("Location", this.Location);
        }
        //This get overwritten is sub classes
        public override void Decode(IDecoder decoder)
        {
            this.CodeType = decoder.ReadString("CodeType");
            this.Timestamp = decoder.ReadDateTime("Timestamp");

            this.ScanData = decoder.ReadExtensionObject<ScanData>("ScanData");
            this.Location = decoder.ReadExtensionObject<Location>("LocationSpecified");
        }

         public override string ToString() => $"{{ CodeType={this.CodeType}; Location={this.Location}; TimeStamp={this.Timestamp}; ScanData={this.ScanData};}}";
    }
}
