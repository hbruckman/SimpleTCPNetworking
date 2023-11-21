namespace SimpleTCPNetworking;

using System.Net;
using System.Net.Sockets;

public class SimpleTCPServer
{
	private TcpListener server;
	private List<Task> pendingTasks;
	private bool isListening;

	public SimpleTCPServer()
	{
		IPAddress[] ipAddresses = Dns.GetHostAddresses("localhost");
		server = new TcpListener(ipAddresses[0], 8080);
		pendingTasks = new List<Task>();
		isListening = false;
	}

	public void StartListening()
	{
		Console.WriteLine("### Begin Start Listening ###");

		server.Start();
		isListening = true;

		while (isListening)
		{
			try
			{
				TcpClient client = server.AcceptTcpClient();
				pendingTasks.Add(Task.Run(() => HandleClient(client)));
			}
			catch
			{
			
			}
		}

		Console.WriteLine("### End Start Listening ###");
	}

	public void StopListening()
	{
		Console.WriteLine("### Begin Stop Listening ###");

		isListening = false;
		server.Stop();

		Task.WaitAll(pendingTasks.ToArray());

		Console.WriteLine("### End Stop Listening ###");
	}

	private void HandleClient(TcpClient client)
	{
		Console.WriteLine("### Begin Handle Client ###");

		using (client)
		{
			NetworkStream ns = client.GetStream();

			BinaryReader br = new BinaryReader(ns);
			BinaryWriter bw = new BinaryWriter(ns);

			string name = br.ReadString();
			bw.Write($"Hi, {name}!");

		} // Does client.Dispose() even if an error occurs.

		Console.WriteLine("### End Handle Client ###");
	}
}
