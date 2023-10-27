namespace SimpleTCPNetworking;

public class Program
{
	public static void Main(string[] args)
	{
		SimpleTCPServer server = new SimpleTCPServer();
		SimpleTCPClient client1 = new SimpleTCPClient("Jane");
		SimpleTCPClient client2 = new SimpleTCPClient("John");

		Task serverTask = Task.Run(() => server.StartListening());
		Task client1Task = Task.Run(() => client1.ConnectToServer());
		Task client2Task = Task.Run(() => client2.ConnectToServer());

		Task.WaitAll(client1Task, client2Task);

		server.StopListening();
	}
}
