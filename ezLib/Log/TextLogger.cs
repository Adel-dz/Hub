using System;
using System.Collections.Generic;
using System.Text;


namespace easyLib.Log
{
    public enum LogSeverity
    {
        Infomational,
        Cautionary,
        Severe,
        Critical,
        Error = Severe,
        Warning = Cautionary,
    }


    public sealed class TextLogger : IDisposable
    {
        readonly static List<ILogReceiver> m_receivers = new List<ILogReceiver>();
        readonly StringBuilder m_msgBuilder;
        readonly LogSeverity m_logSeverity;


        public TextLogger(LogSeverity severity = LogSeverity.Infomational)
        {
            System.Diagnostics.Debug.Assert(Enum.IsDefined(typeof(LogSeverity), severity));

            m_msgBuilder = new StringBuilder();
            m_logSeverity = severity;
        }
                

        public TextLogger PutLine(string msg = null) => Put(msg + Environment.NewLine);
        public TextLogger PutLine(string frmString , params object[] args) => Put(frmString + Environment.NewLine , args);
        public TextLogger PutLine(object obj) => PutLine(obj.ToString());
        
        public TextLogger Put(string msg)
        {
            if (msg != null)
                m_msgBuilder.Append(msg);

            return this;
        }

        public TextLogger Put(object obj)
        {
            if (obj != null)
                Put(obj.ToString());

            return this;
        }

        public TextLogger Put(string frmString, params object[] args)
        {
            string msg = args.Length > 0 ? string.Format(frmString, args) : frmString;

            return Put(msg);
        }

        public void Flush()
        {
            string str = m_msgBuilder.ToString();
            m_msgBuilder.Clear();

            if (!string.IsNullOrWhiteSpace(str))
                Emit(str , m_logSeverity);                
        }

        public void Dispose()
        {
            Flush();
        }

        public static void RegisterReceiver(ILogReceiver receiver)
        {
            System.Diagnostics.Debug.Assert(receiver != null);
            System.Diagnostics.Debug.Assert(!IsReceiverRegistered(receiver));

            m_receivers.Add(receiver);

            System.Diagnostics.Debug.Assert(IsReceiverRegistered(receiver));
        }

        public static void UnregisterReceiver(ILogReceiver receiver)
        {
            System.Diagnostics.Debug.Assert(IsReceiverRegistered(receiver));

            m_receivers.Remove(receiver);

            System.Diagnostics.Debug.Assert(!IsReceiverRegistered(receiver));
        }

        public static bool IsReceiverRegistered(ILogReceiver receiver)
        {
            return receiver != null && m_receivers.Contains(receiver);
        }

        public static void Error(string fmtString, params object[] args)
        {
            string msg = args.Length > 0 ? string.Format(fmtString, args) : fmtString;

            if (!string.IsNullOrWhiteSpace(msg))
                Emit(msg, LogSeverity.Error);
        }

        public static void Warning(string fmtString, params object[] args)
        {
            string msg  = args.Length > 0 ? string.Format(fmtString, args) : fmtString;

            if (!string.IsNullOrWhiteSpace(msg))
                Emit(msg, LogSeverity.Warning);
        }        

        public static void Info(string fmtString, params object[] args)
        {
            string msg = args.Length > 0 ? string.Format(fmtString, args) : fmtString;

            if (!string.IsNullOrWhiteSpace(msg))
                Emit(msg, LogSeverity.Infomational);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Debug(string fmtString, params object[] args)
        {
            string msg = args.Length > 0 ? string.Format(fmtString, args) : fmtString;
            System.Diagnostics.Debug.WriteLine(msg);
        }


        //private:        
        static void Emit(string msg, LogSeverity severity)
        {
            System.Diagnostics.Debug.WriteLine(msg);

            lock (m_receivers)
                m_receivers.ForEach(r => r.Write(msg, severity));
        }
    }
}
