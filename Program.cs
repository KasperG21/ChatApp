namespace Program
{
    public class Program
    {
        private static Thread[] threads = new Thread[2];
        private static TUI? tui;

        public static void Main()
        {
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => OnExit();
            
            try { tui = new TUI("127.0.0.1", "4000"); }
            catch (IOException) { Console.WriteLine("Please use another terminal."); return; };

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

            try
            {
                client.Connect("127.0.0.1", 4000);
            }
            catch (System.Net.Sockets.SocketException)
            {
                tui.Write("Couldn't connect to server, make sure you are connected to internet.\n", isError: true);
                tui.Write("If this problem keeps occuring, it probably means that the server you are trying to connect to is offline.\n", isError: true);
                tui.Write("Press any key to continue...\n", isError: true);
                Console.ReadKey(true);
                OnExit();
                return;
            }

            if (!tui.Connected())
            {
                tui.Exit();
                return;
            }
            
            System.Net.Sockets.NetworkStream stream = client.GetStream();
            new System.Net.Sockets.TcpClient().ReceiveBufferSize = 132;

            threads[0] = new Thread(() => NetworkIO.Listen(stream, tui));
            threads[1] = new Thread(() => NetworkIO.Write(stream, tui));
            foreach (Thread t in threads) t.Start();
            foreach (Thread t in threads) t.Join();

            OnExit();
        }

        private static void OnExit()
        {
            tui?.Exit();
        }
    }
}