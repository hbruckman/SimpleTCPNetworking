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
		Console.WriteLine("### Begin Connect to Server ###");

		using (client)
		{
			client.Connect("localhost", 8080);

			NetworkStream ns = client.GetStream();

			BinaryReader br = new BinaryReader(ns);
			BinaryWriter bw = new BinaryWriter(ns);

			bw.Write(name);
			string greeting = br.ReadString();

			Console.WriteLine(greeting);

		} // Does client.Dispose() even if an error occurs.

		Console.WriteLine("### End Connect to Server ###");
	}
}
