using DGD.HubCore.Arch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Arch
{
    sealed partial class ArchiveViewer: Form
    {
        readonly IArchiveContent m_archData;


        public ArchiveViewer(IArchiveContent archContent)
        {
            InitializeComponent();

            m_archData = archContent;
        }


        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_lbData.BeginUpdate();

            m_lbData.Items.Add($"* Archive créee le {m_archData.CreationTime}");
            m_lbData.Items.Add($"* Version des données: {BitConverter.ToUInt32(m_archData.ArchiveHeader, 0)}");
            m_lbData.Items.Add(" ");
            m_lbData.Items.Add("* Liste des fichiers:");

            foreach(string file in m_archData.Files)
                m_lbData.Items.Add(file);

            m_lbData.EndUpdate();

            base.OnLoad(e);
        }
    }
}
