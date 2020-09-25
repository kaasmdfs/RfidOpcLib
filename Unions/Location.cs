using RfidOpcLib.Structures;
using Workstation.ServiceModel.Ua;

namespace RfidOpcLib.Unions
{
    [DataTypeId("ns=3;i=6034")]
    [BinaryEncodingId("ns=3;i=5013")]
    public class Location : Union
    {
        public int? SwitchField { get; set; }
        public string NMEA { get; set; }
        public LocalCoordinate Local { get; set; }
        public WGS84Coordinate WGS84 { get; set; }
        public string Name { get; set; }

        public override void Encode(IEncoder encoder)
        {
            encoder.WriteInt32("SwitchField", this.SwitchField.Value);
        }

        public override void Decode(IDecoder decoder)
        {
            this.SwitchField = decoder.ReadInt32("SwitchField");
            switch (SwitchField)
            {
                case 0:

                    break;
                case 1:
                    this.NMEA = decoder.ReadString("NMEA");
                    break;
                case 2:
                    this.Local = decoder.ReadExtensionObject<LocalCoordinate>("Local");
                    break;
                case 3:
                    this.WGS84 = decoder.ReadExtensionObject<WGS84Coordinate>("WGS84");
                    break;
                case 4:
                    this.Name = decoder.ReadString("Name");
                    break;
                default:
                    this.SwitchField = 0;
                    break;
            }

        }

        //public override string ToString() => $"{{ SwitchField={this.SwitchField}; String={this.String};}}";
    }
}
