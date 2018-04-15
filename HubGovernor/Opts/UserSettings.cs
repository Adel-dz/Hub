using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Opts
{
    sealed class UserSettings
    {
        public class WindowPlacementCollection
        {
            readonly UserSettings m_owner;

            public WindowPlacementCollection(UserSettings owner)
            {
                m_owner = owner;
            }


            public WindowPlacement this[string key]
            {
                get
                {
                    WindowPlacement wp;
                    if (!m_owner.m_windPlacements.TryGetValue(key , out wp))
                        return null;

                    return m_owner.m_windPlacements[key];
                }

                set
                {
                    if (value == null)
                        m_owner.m_windPlacements.Remove(key);
                    else
                        m_owner.m_windPlacements[key] = value;
                }
            }
        }


        readonly Dictionary<string , WindowPlacement> m_windPlacements = new Dictionary<string , WindowPlacement>();


        public UserSettings()
        {
            DSVImportSettings = new DSVImportSettings();
            BackupFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public bool LogWindowHidden { get; set; }
        public DSVImportSettings DSVImportSettings { get; }
        public WindowPlacementCollection WindowPlacement => new WindowPlacementCollection(this);
        public string BackupFolder { get; set; }

        public void Load(BinaryReader reader)
        {
            DSVImportSettings.Load(reader);
            LogWindowHidden = reader.ReadBoolean();

            m_windPlacements.Clear();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; ++i)
            {
                string key = reader.ReadString();
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                int w = reader.ReadInt32();
                int h = reader.ReadInt32();

                m_windPlacements[key] = new WindowPlacement(x , y , w , h);
            }
        }

        public void Save(BinaryWriter writer)
        {
            DSVImportSettings.Save(writer);
            writer.Write(LogWindowHidden);

            writer.Write(m_windPlacements.Count);

            foreach (string key in m_windPlacements.Keys)
            {
                WindowPlacement wp = m_windPlacements[key];

                writer.Write(key);
                writer.Write(wp.Left);
                writer.Write(wp.Top);
                writer.Write(wp.Width);
                writer.Write(wp.Height);
            }
        }

        public void Reset()
        {
            LogWindowHidden = false;
            DSVImportSettings.Reset();
            m_windPlacements.Clear();
        }
    }
}
