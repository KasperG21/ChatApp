namespace Program
{
    partial class TUI
    {
        private string ipAddr, port;
        private string originalWindowTitle, typingBuffer;
        private (int left, int top) lastServerMessageLocation, lastUserMessageLocation = (left: 0, top: 0);
        private Queue<string> messages = [];
    }
}