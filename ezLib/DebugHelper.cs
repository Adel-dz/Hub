using easyLib.Log;
using easyLib.DB;
using System.Collections.Generic;
using System.Diagnostics;



namespace easyLib
{
    public static class DebugHelper
    {
        readonly static Dictionary<string , List<IDatumProvider>> m_tableProviders = new Dictionary<string , List<IDatumProvider>>();
        
                
        [Conditional("DEBUG")]
        public static void RegisterProvider(IDatumProvider provider, string tableName)
        {
            Debug.Assert(provider != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(tableName));

            lock(m_tableProviders)
            {
                if (!m_tableProviders.ContainsKey(tableName))
                    m_tableProviders.Add(tableName, new List<IDatumProvider>());

                m_tableProviders[tableName].Add(provider);
                
                //EventLogger.Debug("Table: {0}, +dp, # = {1}.", tableName, m_tableProviders[tableName].Count);
            }
        }

        [Conditional("DEBUG")]
        public static void RegisterProvider(IDatumProvider provider, IDatumProvider dpSame)
        {
            Debug.Assert(provider != null);
            Debug.Assert(dpSame != null);        
            

            lock(m_tableProviders)
            {
                Debug.Assert(IsProviderRegistered(dpSame));

                string tblName = Locate(dpSame);
                m_tableProviders[tblName].Add(provider);

                //EventLogger.Debug("Table: {0}, +dp, # = {1}.", tblName, m_tableProviders[tblName].Count);
            }
        }

        [Conditional("DEBUG")]
        public static void UnregisterProvider(IDatumProvider provider)
        {
            if(provider != null)
                lock (m_tableProviders)
                {
                    string key = Locate(provider);
                    m_tableProviders[key].Remove(provider);

                    //EventLogger.Debug("Table: {0}, -dp , # of dps  = {1}.", key, m_tableProviders[key].Count);
                }
        }        


        [Conditional("DEBUG")]
        public static void AssertAll()
        {
            int dpCount = 0;

            foreach (string  key in m_tableProviders.Keys)            
            {
                int n = m_tableProviders[key].Count;

                if (n != 0)
                    TextLogger.Debug("Warn! Table: {0}, # of dps  = {1}.", key, n);

                dpCount += n;
            }

            Debug.Assert(dpCount == 0);
        }

        [Conditional("DEBUG")]
        public static void LogDbgInfo(string msg) => System.Diagnostics.Debug.WriteLine(">> " + msg);

        [Conditional("DEBUG")]
        public static void Assert(bool exp) => System.Diagnostics.Debug.Assert(exp);


        //private:
        static string Locate(IDatumProvider provider)
        {
            foreach (string key in m_tableProviders.Keys)
                if (m_tableProviders[key].Contains(provider))
                    return key;

            return null;
        }


        static bool IsProviderRegistered(IDatumProvider provider)
        {
            if (provider == null)
                return false;

            lock (m_tableProviders)
            {
                return Locate(provider) != null;
            }
        }

    }





    public static class ProvidersTracker
    {
        readonly static Dictionary<string , List<object>> m_tableProviders = new Dictionary<string , List<object>>();

        //public:
        [Conditional("DEBUG")]
        public static void RegisterProvider<T>(IProvider<T> provider , string tableName)
        {
            Debug.Assert(provider != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(tableName));

            lock (m_tableProviders)
            {
                if (!m_tableProviders.ContainsKey(tableName))
                    m_tableProviders.Add(tableName , new List<object>());

                m_tableProviders[tableName].Add(provider);
            }
        }

        [Conditional("DEBUG")]
        public static void RegisterProvider<T>(IProvider<T> provider , IProvider<T> dpSame)
        {
            Debug.Assert(provider != null);
            Debug.Assert(dpSame != null);


            lock (m_tableProviders)
            {
                Debug.Assert(IsProviderRegistered(dpSame));

                string tblName = Locate(dpSame);
                m_tableProviders[tblName].Add(provider);
            }
        }

        [Conditional("DEBUG")]
        public static void UnregisterProvider<T>(IProvider<T> provider)
        {
            if (provider != null)
                lock (m_tableProviders)
                {
                    string key = Locate(provider);
                    m_tableProviders[key].Remove(provider);
                }
        }


        [Conditional("DEBUG")]
        public static void AssertAll()
        {
            int dpCount = 0;

            foreach (string key in m_tableProviders.Keys)
            {
                int n = m_tableProviders[key].Count;

                if (n != 0)
                    TextLogger.Debug("Warn! Table: {0}, # of dps  = {1}." , key , n);

                dpCount += n;
            }

            Debug.Assert(dpCount == 0);
        }


        //private:
        static string Locate<T>(IProvider<T> provider)
        {
            foreach (string key in m_tableProviders.Keys)
                if (m_tableProviders[key].Contains(provider))
                    return key;

            return null;
        }


        static bool IsProviderRegistered<T>(IProvider<T> provider)
        {
            if (provider == null)
                return false;

            lock (m_tableProviders)
            {
                return Locate(provider) != null;
            }
        }

    }
}
