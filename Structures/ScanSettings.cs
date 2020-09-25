using RfidOpcLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6044")]
    [BinaryEncodingId("ns=3;i=5015")]
    public class ScanSettings :Structure

    {
        public double Duration { get; set; }
        public int Cycles { get; set; }
        public bool DataAvailable { get; set; }
        public LocationTypeEnumeration LocationTypeSpecified { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteDouble("Duration", this.Duration);
            encoder.WriteInt32("Cycles", this.Cycles);
            encoder.WriteBoolean("DataAvailable", this.DataAvailable);
            encoder.WriteEnumeration("LocationTypeSpecified", this.LocationTypeSpecified);
        }

        public override void Decode(IDecoder decoder)
        {
            this.Duration = decoder.ReadDouble("Duration");
            this.Cycles = decoder.ReadInt32("Cycles");
            this.DataAvailable = decoder.ReadBoolean("DataAvailable");
        }

        public override string ToString() => $"{{ Duration={this.Duration}; Cycles={this.Cycles}; DataAvailable={this.DataAvailable}; LocationTypeSpecified={this.LocationTypeSpecified};}}";
    }
}
