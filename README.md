# RFID Methods
This is an expansion set of classes that expands upon the [convertersystems/opc-ua-client](https://github.com/convertersystems/opc-ua-client) libraries that  are used to make communication to a OPC UA server easier inside of C#.  These classes will make OPC UA method calls to Siemens RF600 RFID readers easier to handle. 
### Currently Supported Methods
Scan

### Usage
```csharp
   var rfidReader = new RfidReader("opc.tcp://192.168.0.254:4840"); //Give the opc ua address to the reader 
   rfidReader.Start(); //initialize the reader
   List<RfidScanResult> res =  await rfidReader.Scan(500, 10); //call the scan method and pass through the duration and cycles

```
