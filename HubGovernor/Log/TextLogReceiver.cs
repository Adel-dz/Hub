namespace DGD.HubGovernor.Log
{
    interface ITextLogReceiver
    {
        void Write(string message, LogSeverity severity);
    }
}
