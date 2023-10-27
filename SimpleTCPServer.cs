namespace SimpleTCPNetworking;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

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
	}

	public void StopListening()
	{
		isListening = false;
		server.Stop();

		Task.WaitAll(pendingTasks.ToArray());
	}

	private void HandleClient(TcpClient client)
	{
		NetworkStream ns = client.GetStream();

		BinaryReader br = new BinaryReader(ns);
		BinaryWriter bw = new BinaryWriter(ns);

		string name = br.ReadString();
		bw.Write($"Hi, {name}!");

		client.Dispose();
	}
}
