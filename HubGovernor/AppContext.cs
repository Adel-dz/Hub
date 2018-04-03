using DGD.HubCore.DB;
using DGD.HubGovernor.Clients;
using DGD.HubGovernor.DB;
using DGD.HubGovernor.Opts;
using DGD.HubGovernor.TR;
using DGD.HubGovernor.Updating;
using System.IO;

namespace DGD.HubGovernor
{
    static class AppContext
    {
        const string APP_BASE_DIR = "DGD.Governor";

        static readonly Log.LogManager m_logManager;
        static readonly TableManager m_tableManager;
        static readonly ClientsManager m_clientsManager;
        static readonly DataAccessPath m_accessPath;
        readonly static Settings m_settings;
        readonly static DataRowFactory m_datumFactory;        
        static TransactionListener m_ioListener;
        

        static AppContext()
        {
            m_logManager = new Log.LogManager();
            m_tableManager = new TableManager();
            m_settings = new Settings();
            m_accessPath = new DataAccessPath();
            m_datumFactory = new DataRowFactory();
            m_clientsManager = new ClientsManager();
        }


        public static bool IsDisposed { get; private set; }
        public static TableManager TableManager => m_tableManager;        
        public static Settings Settings => m_settings;
        public static DataAccessPath AccessPath => m_accessPath;
        public static IDatumFactory DatumFactory => m_datumFactory;
        public static ClientsManager ClientsManager => m_clientsManager;
        public static TransactionListener TransactionListener => m_ioListener;
        public static Log.LogManager LogManager => m_logManager;


        public static void Init()
        {
            m_ioListener = new TransactionListener();
            m_ioListener.Start();

            try
            {
                m_settings.Load();
            }
            catch
            {
                m_settings.Reset();
            }

        }

        public static void Dispose()
        {
            if (!IsDisposed)
            {
                m_clientsManager.Dispose();
                m_ioListener.Dispose();
                m_accessPath.Dispose();
                m_logManager.Dispose();
                m_tableManager.Dispose();
                m_settings.Save();

                IsDisposed = true;
            }
        }


        //private:
    }
}
