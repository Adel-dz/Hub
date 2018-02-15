namespace easyLib.Log
{
    public interface ILogReceiver
    {
        void Write(string message, LogSeverity severity);
    }
}
