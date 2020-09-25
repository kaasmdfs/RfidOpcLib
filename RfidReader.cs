using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.X509;
using RfidOpcLib.Structures;
using RfidOpcLib.Unions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace RfidOpcLib
{
    public class RfidReader
    {
        protected readonly ApplicationDescription localDescription;
        protected readonly ICertificateStore certificateStore;
        protected readonly X509Identity x509Identity;
        protected UaTcpSessionChannel channel;
        protected string _opcAddress { get; set; }
       
        public RfidReader(string opcAddress)
        {
            _opcAddress = opcAddress;          

            this.localDescription = new ApplicationDescription
            {
                ApplicationName = "RfidReader",
                ApplicationUri = $"urn:{Dns.GetHostName()}:Workstation.RfidReader",
                ApplicationType = ApplicationType.Client
            };

            var pkiPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Workstation.RfidReader",
                    "pki");
            this.certificateStore = new DirectoryStore(pkiPath);

            // read x509Identity
            var userCert = default(X509Certificate);
            var userKey = default(RsaKeyParameters);

            var certParser = new X509CertificateParser();
            var userCertInfo = new FileInfo(Path.Combine(pkiPath, "user", "certs", "ctt_usrT.der"));
            if (userCertInfo.Exists)
            {
                using (var crtStream = userCertInfo.OpenRead())
                {
                    var c = certParser.ReadCertificate(crtStream);
                    if (c != null)
                    {
                        userCert = c;
                    }
                }
            }
            var userKeyInfo = new FileInfo(Path.Combine(pkiPath, "user", "private", "ctt_usrT.pem"));
            if (userKeyInfo.Exists)
            {
                using (var keyStream = new StreamReader(userKeyInfo.OpenRead()))
                {
                    var keyReader = new PemReader(keyStream);
                    var keyPair = keyReader.ReadObject() as AsymmetricCipherKeyPair;
                    if (keyPair != null)
                    {
                        userKey = keyPair.Private as RsaKeyParameters;
                    }
                }
            }
            if (userCert != null && userKey != null)
            {
                x509Identity = new X509Identity(userCert, userKey);
            }
        }
        public  void Start()
        {
             channel = new UaTcpSessionChannel(
              this.localDescription,
              null,
              new AnonymousIdentity(),
             _opcAddress,
              SecurityPolicyUris.None,
              loggerFactory: null,
              additionalTypes: new[] { typeof(ScanSettings)
              , typeof(ScanResult), typeof(RfidScanResult),  typeof(ScanData),
                typeof(Location),typeof(WGS84Coordinate), typeof(RfidSighting),typeof(LocalCoordinate), typeof(ScanDataEpc)
              });

        }
        public async Task<List<RfidScanResult>> Scan(double duration, int cycles)
        {
            try
            {
                //opening a session
                await channel.OpenAsync();

                Console.WriteLine($"Opened session with endpoint '{channel.RemoteEndpoint.EndpointUrl}'.");
                Console.WriteLine($"SecurityPolicy: '{channel.RemoteEndpoint.SecurityPolicyUri}'.");
                Console.WriteLine($"SecurityMode: '{channel.RemoteEndpoint.SecurityMode}'.");
                Console.WriteLine($"UserIdentityToken: '{channel.UserIdentity}'.");

                var set = new ScanSettings
                {
                    Duration = duration,
                    Cycles = cycles,
                    DataAvailable = false
                };
                var request = new CallRequest
                {
                    MethodsToCall = new[] {
                        new CallMethodRequest
                        {
                            ObjectId =  NodeId.Parse("ns=2;i=5002"),
                            MethodId =  NodeId.Parse("ns=4;i=7010"),
                            InputArguments = new [] { new ExtensionObject(set)}.ToVariantArray()
                        }
                    }
                };

                var response = await channel.CallAsync(request);
                //Get the result from the server
                var result = response.Results[0].OutputArguments[0].GetValue();
                //Convert the result to the array that it is
                var resultArray = (object[])result;
                var scanResults = new List<RfidScanResult>();
                foreach (var res in resultArray)
                {
                    var scanResult = (RfidScanResult)res;
                    scanResults.Add(scanResult);
                }
                Console.WriteLine($"\nClosing session '{channel.SessionId}'.");
                await channel.CloseAsync();
                return scanResults;
            }
            catch (Exception ex)
            {
                await channel.AbortAsync();
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
