namespace Program
{
    partial class TUI
    {
        private string ipAddr, port;
        private string originalWindowTitle, typingBuffer;
        private (int left, int top) lastServerMessageLocation, lastUserMessageLocation = (left: 0, top: 0);
        private Queue<string> messages = [];

        private bool DrawInputField()
        {
            if (GetHeight() < 16 && GetWidth() < 28)
            {
                Console.WriteLine("The window in use is not big enough to support proper working of the TUI, please use a bigger window.");
                return false;
            }

            for (int i = GetHeight() - 1; i > GetHeight() - 5; i --) WriteCharAt('│', 1, i);
            WriteCharAt('┌', 1, GetHeight() - 6);
            for (int i = 2; i < GetWidth() - 3; i ++) WriteCharAt('─', i, GetWidth() - 6);
            WriteCharAt('┐', GetWidth() - 2, GetHeight() - 6);
            for (int i = GetHeight() - 1; i > GetHeight() - 5; i --) WriteCharAt('│', GetWidth() - 2, i);

            return true;
        }

        private void WriteContents(bool isError = false, bool isUser = false)
        {
            if (isUser)
            {

            }
        }

        private void WriteCharAt(char c, int x, int y)
        {
            // Console.SetCursorPosition(x, y);
            Console.SetCursorPosition(0, 0);
            Console.Write(c);
        }

        private int GetWidth()
        {
            return Console.WindowWidth;
        }

        private int GetHeight()
        {
            return Console.WindowHeight;
        }
    }
}