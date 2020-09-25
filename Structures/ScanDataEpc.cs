using System;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Structures
{
    [DataTypeId("ns=3;i=6138")]
    [BinaryEncodingId("ns=3;i=5015")]
    public class ScanDataEpc : Structure
    {
        public UInt16 pC { get; set; }
        public byte[] uId { get; set; }
        public UInt16 xPC_W1 { get; set; }
        public UInt16 xPC_W2 { get; set; }
        public override void Encode(IEncoder encoder)
        {
            encoder.WriteUInt16("pC", this.pC);
            encoder.WriteUInt16("xPC_W1", this.xPC_W1);
            encoder.WriteUInt16("xPC_W2", this.xPC_W2);
            encoder.WriteByteString("uId", this.uId);
        }

        public override void Decode(IDecoder decoder)
        {
            this.pC = decoder.ReadUInt16("pC");
            this.xPC_W1 = decoder.ReadUInt16("xPC_W1");
            this.xPC_W2 = decoder.ReadUInt16("xPC_W2");
            this.uId = decoder.ReadByteString("uId");
        }

        public override string ToString() => $"{{ pC={this.pC}; xPC_W1={this.xPC_W1}; xPC_W2={this.xPC_W2}; uId={this.uId};}}";
    }
}
