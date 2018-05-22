using DGD.HubCore.RunOnce;
using easyLib;
using easyLib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.Hub.RunOnce
{
    sealed class RunOnceManager : IDisposable
    {
        const string SIGNATURE = "HUBRO1";

        readonly List<IRunOnceAction> m_actions = new List<IRunOnceAction>();
        readonly Dictionary<RunOnceAction_t, Func<IRunOnceAction>> m_actBuilder;


        public RunOnceManager()
        {
            m_actBuilder = new Dictionary<RunOnceAction_t , Func<IRunOnceAction>>
            {
                { RunOnceAction_t.DeleteFile, () => new DeleteFile() },
                { RunOnceAction_t.ResetClientInfo, () => new ResetClientInfo() },
                { RunOnceAction_t.ResetUpdateInfo, () => new ResetUpdateInfo() }
            };


            Load();
        }

        public bool IsDisposed { get; private set; }

        public void Add(IRunOnceAction action)
        {
            Dbg.Assert(action != null);

            m_actions.Add(action);
        }

        public void Add(IEnumerable<IRunOnceAction> actions)
        {
            Dbg.Assert(actions != null);

            m_actions.AddRange(actions);
        }

        public void AddFirst(IRunOnceAction action)
        {
            Dbg.Assert(action != null);

            m_actions.Insert(0 , action);
        }

        public void AddFirst(IEnumerable<IRunOnceAction> actions)
        {
            Dbg.Assert(actions != null);

            m_actions.InsertRange(0 , actions);
        }

        public void  Save()
        {
            using (FileStream fs = File.Create(SettingsManager.RunOnceFilePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(Signature);
                writer.Write(m_actions.Count);

                foreach (IRunOnceAction act in m_actions)
                {
                    writer.Write((int)act.ActionCode);
                    act.Write(writer);
                }
            }
        }

        public void Run(Form form = null)
        {
            Action run = () =>
            {
                while (m_actions.Count > 0)
                {
                    try
                    {
                        m_actions[0].Run();
                    }
                    catch (Exception ex)
                    {
                        Dbg.Log(ex.Message);
                    }
                    finally
                    {
                        m_actions.RemoveAt(0);
                    }
                }
            };

            var dlg = new BusyDialog();

            var task = new Task(run , TaskCreationOptions.LongRunning);
            task.OnSuccess(dlg.Dispose);
            task.Start();

            if (form != null)
                dlg.ShowDialog(form);
            else
                dlg.ShowDialog();

            Save();
        }

        public void Dispose()
        {
            if(!IsDisposed)
            {
                Save();
                IsDisposed = true;
            }
        }

        //private:
        byte[] Signature => Encoding.UTF8.GetBytes(SIGNATURE);

        void Load()
        {
            FileStream fs = null;            

            try            
            {
                fs = File.OpenRead(SettingsManager.RunOnceFilePath);
                var reader = new RawDataReader(fs , Encoding.UTF8);

                byte[] sign = Signature;

                foreach (byte b in sign)
                    if (b != reader.ReadByte())
                        throw new CorruptedFileException(SettingsManager.RunOnceFilePath);

                int actCount = reader.ReadInt();
                for(int i = 0; i < actCount;++i)
                {
                    var code = (RunOnceAction_t)reader.ReadInt();
                    IRunOnceAction act = m_actBuilder[code]();
                    act.Read(reader);
                    m_actions.Add(act);
                }
                        
            }
            catch(Exception ex)
            {
                Dbg.Log(ex.Message);
                m_actions.Clear();
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }

        }
    }
}
