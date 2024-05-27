using System.Net.Sockets;
using System.Text;

namespace Program
{
    static class NetworkIO
    {
        public static string customHelloMessage = "Present!";

        public static void Listen(NetworkStream stream, TUI tui)
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[132];
                    int bytesRead = stream.Read(buffer, 0, 132);

                    tui.Write($"{Encoding.UTF8.GetString(buffer[0..+ bytesRead])}\n");
                }
                catch (IOException)
                {
                    tui.Write("The server in use broke the connection.\n", isError: true);
                    tui.Write("Press any key to continue...\n", isError: true);
                    stream.Close();
                    stream.Dispose();
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }

        public static void Write(NetworkStream stream, TUI tui)
        {
            stream.Write(Encoding.UTF8.GetBytes(customHelloMessage));
            
            while (true)
            {
                try
                {
                    char inputChar = Console.ReadKey(true).KeyChar;
                    if (inputChar == (char)13)
                    {
                        if (tui.TypingBuffer.Length > 128) { tui.TypingBuffer = ""; tui.Write("ERROR: There is a character limit of 128 on the messages you want to send.\n", isError: true); }
                        else { stream.Write(Encoding.UTF8.GetBytes(new string(tui.TypingBuffer.Concat("\n\r").ToArray()))); tui.TypingBuffer = ""; }
                    }
                    else if (inputChar == (char)8) tui.TypingBuffer = tui.TypingBuffer.Remove(tui.TypingBuffer.Length-2);
                    else if (inputChar == '\n') { tui.Write("ERROR: The character \'\\n\'n is illegal and shall not be used in any message.\n", isError: true); tui.TypingBuffer = ""; }
                    else tui.Write(inputChar.ToString(), isUser: true);
                }
                catch (IOException)
                {
                    tui.Write("The server in use broke the connection.\n", isError: true);
                    tui.Write("Press any key to continue...\n", isError: true);
                    stream.Close();
                    stream.Dispose();
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }
    }
}