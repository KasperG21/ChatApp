namespace Program
{
    partial class TUI
    {
        public string TypingBuffer { get {return typingBuffer;} set {typingBuffer = value;} }

        public TUI(string ipAddr, string port)
        {
            this.ipAddr = ipAddr;
            this.port = port;
            typingBuffer = "";
            originalWindowTitle = OperatingSystem.IsWindows()? Console.Title : "";

            Console.Clear();
            Console.Title = $"GuChat_ - Connecting to {this.ipAddr}:{this.port}";
            Console.CursorVisible = false;
        }

        public bool Connected()
        {
            Console.Title = $"GuChat_ - Chatting on {this.ipAddr}:{this.port}";

            throw new NotImplementedException();
        }

        public void Write(string s, bool isError = false, bool isUser = false)
        {
            if (!isUser) messages.Enqueue(s);
            // The inputstring for users will always be just one character
            else typingBuffer += s[0];
        }
        
        public void Exit()
        {
            Console.Title = originalWindowTitle;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}