using RfidOpcLib.Structures;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Unions
{
    [DataTypeId("ns=3;i=6131")]
    [BinaryEncodingId("ns=3;i=5030")]
    public class ScanData : Union
    {
        public int SwitchField { get; set; }
        public string String { get; set; }
        public byte[] ByteString { get; set; }
        public ScanDataEpc Epc { get; set; }
        public Variant Custom { get; set; }


        public override void Encode(IEncoder encoder)
        {
            encoder.WriteInt32("SwitchField", this.SwitchField);
        }

        public override void Decode(IDecoder decoder)
        {
            this.SwitchField = decoder.ReadInt32("SwitchField");
            switch (SwitchField)
            {
                case 1:
                    this.ByteString = decoder.ReadByteString("ByteString");
                    break;
                case 2:
                    this.String = decoder.ReadString("String");
                    break;
                case 3:
                    this.Epc = decoder.ReadExtensionObject<ScanDataEpc>("Epc");
                    break;
                case 4:
                    this.Custom = decoder.ReadVariant("Custom");
                    break;
                default:
                    break;
            }

        }

        // public override string ToString() => $"{{ SwitchField={this.SwitchField}; String={this.String};}}";
    }
}
