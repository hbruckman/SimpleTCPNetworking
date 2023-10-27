namespace SimpleTCPNetworking;

using System.Net.Sockets;

public class SimpleTCPClient
{
	private TcpClient client;
	private string name;

	public SimpleTCPClient(string name)
	{
		client = new TcpClient();
		this.name = name;
	}

	public void ConnectToServer()
	{
		client.Connect("localhost", 8080);

		NetworkStream ns = client.GetStream();

		BinaryReader br = new BinaryReader(ns);
		BinaryWriter bw = new BinaryWriter(ns);

		bw.Write(name);
		string greeting = br.ReadString();

		Console.WriteLine(greeting);

		client.Dispose();
	}
}
