ByteNom
=======

Simple to use protocol for client server communication.


## Client
To connect to a ByteNom server you need to use the `Client` class.
```csharp
var client = new Client(host, port);
client.MessageReceived += (sender, message) =>
{
	// TODO: Handle incoming messages
};
client.Connect();

// Send initial messages to the server here
client.Send("test");
```

## Server
A ByteNom server handles incoming messages from ByteNom clients.
```csharp
var server = new Server(port);
server.ConnectionReceived += (sender, connection) =>
{
    connection.MessageReceived += (sender2, message) =>
    {
        // TODO: Handle incoming messages
    };   
    // Send initial messages to the client here
    connection.Send("version", 1);
};
server.Start();
```

## Messages
All communication between server and client is done through Message classes. A messages consist of a type, and a variable number of arguments.
The type of a message is a string and can be anything you want. You can check for the type of the messages to know how to handle them.
Messages can contain as many arguments as you want. By default, ByteNom supports the following types:

- bool
- chars, strings
- int, short, long and also uint, ushort and ulong
- float, double, decimal
- bytes and sbytes
- DateTime
- nested Messages

Additionally, arrays of all types above are also supported.

## Sending your own classes
ByteNom uses Protobuf to serialize classes. To make it possible for a class to be sent over protobuf, an ID must be associated with it. You can bind this ID to your class using the `MessageSerializer.RegisterType` method:

```csharp
MessageSerializer.RegisterType(100, typeof(CustomClass1));
MessageSerializer.RegisterType(101, typeof(CustomClass2));
```
Note that each class must be associated with a unique identifier. This identifier must match on both the client and server sides.
