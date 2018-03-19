using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGD.HubCore.DLG;
using easyLib.DB;
using System.IO;
using easyLib;
using DGD.HubCore.Net;
using easyLib.Extensions;
using System.Text.RegularExpressions;
using System.Threading;

namespace DGD.Hub.Opts
{
    sealed partial class SettingsView: UserControl, IView
    {
        public static event Action ClientInfoChanged;
        public static event Action CountryPrefernceChanged;

        public SettingsView()
        {
            InitializeComponent();

        }


        public void Activate(Control parent)
        {
            Dbg.Assert(parent != null);

            SettingsManager opt = Program.Settings;
            m_chkUseInternalCode.Checked = opt.UseCountryCode;

            parent.Controls.Add(this);
            Dock = DockStyle.Fill;

            Show();
        }

        public void Deactivate(Control parent)
        {
            Dbg.Assert(parent != null);

            SettingsManager opt = Program.Settings;

            bool useCtryCode = m_chkUseInternalCode.Checked;

            if (opt.UseCountryCode != useCtryCode)
            {
                opt.UseCountryCode = useCtryCode;
                CountryPrefernceChanged?.Invoke();
            }

            parent.Controls.Remove(this);
            Hide();
        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            ClientInfo clInfo = Program.Settings.ClientInfo;

            if (clInfo != null)
            {
                m_tbContact.Text = clInfo.ContactName;
                m_tbEmail.Text = clInfo.ContaclEMail;
                m_tbPhone.Text = clInfo.ContactPhone;

                Func<string> dlProfile = () =>
                {
                    string tmpFile = Path.GetTempFileName();

                    try
                    {
                        new NetEngin(Program.Settings).Download(tmpFile , SettingsManager.ProfilesURI);
                        ProfileInfo pi = DialogEngin.ReadProfiles(tmpFile).SingleOrDefault(p =>
                            p.ProfileID == clInfo.ProfileID);

                        return pi?.ProfileName;
                    }
                    catch { }
                    finally
                    {
                        File.Delete(tmpFile);
                    }

                    return null;
                };


                Action<Task<string>> onSucces = t =>
                {
                    m_lblProfile.Text = t.Result;
                };

                var task = new Task<string>(dlProfile , TaskCreationOptions.LongRunning);
                task.OnSuccess(onSucces);
                task.Start();
            }


            base.OnLoad(e);
        }


        //private:
        void UpdateUI()
        {
            Dbg.Assert(!InvokeRequired);

            m_btnSave.Enabled = !string.IsNullOrWhiteSpace(m_tbContact.Text) &&
                (!string.IsNullOrWhiteSpace(m_tbEmail.Text) ||
                !string.IsNullOrWhiteSpace(m_tbPhone.Text));
        }

        bool DataChanged()
        {
            ClientInfo clInfo = Program.Settings.ClientInfo;

            Dbg.Assert(clInfo != null);

            return m_tbContact.Text.Trim() != clInfo.ContactName ||
                m_tbEmail.Text.Trim() != clInfo.ContaclEMail ||
                m_tbPhone.Text.Trim() != clInfo.ContactPhone;
        }

        bool ValidatePhoneNumber(string str)
        {
            const string pattern = @"^0(([5-7]\d{8})|([2-4]\d{7}))$";
            var re = new Regex(pattern);

            return re.IsMatch(str);
        }

        bool ValidateEMail(string str)
        {
            const string pattern = @"^[a-z\.\-_]+\S*@[a-z]+\S*\.[a-z]+\S*$";
            var re = new Regex(pattern , RegexOptions.IgnoreCase);

            return re.IsMatch(str);
        }

        void PostSetInfoMessage()
        {
            ClientInfo curClInfo = Program.Settings.ClientInfo;
            var clInfo = new ClientInfo(curClInfo.ClientID , curClInfo.ProfileID);
            clInfo.ContaclEMail = m_tbEmail.Text.Trim();
            clInfo.ContactName = m_tbContact.Text.Trim();
            clInfo.ContactPhone = m_tbPhone.Text.Trim();

            byte[] msgData = clInfo.GetBytes();

            var dlg = new BusyDialog();

            Func<bool> postMessage = () =>
            {
                const int TIME_TO_SLEEP = 10 * 1000;
                int nbAttempts = 3;


                while (--nbAttempts >= 0)
                {
                    dlg.Message = "Envoi  des données vers le serveur...";
                    uint msgID = Program.DialogManager.SendMessage(Message_t.SetInfo , msgData);

                    if (msgID == 0)
                        continue;

                    dlg.Message = "En attente de la réponse du serveur...";
                    Thread.Sleep(TIME_TO_SLEEP);

                    dlg.Message = "Réception des données à partir du serveur...";
                    HubCore.DLG.Message resp = Program.DialogManager.ReceiveMessage(msgID);

                    Dbg.Assert(resp == null || resp.MessageCode == Message_t.Ok);

                    if (resp != null)
                    {
                        dlg.Message = "Transfert terminé.";
                        break;
                    }
                }

                return nbAttempts >= 0;
            };

            Action<Task> onErr = t =>
            {
                dlg.Dispose();

                if (t.Exception != null)
                    Dbg.Log(t.Exception.InnerException.Message);

                MessageBox.Show("Impossible de se connecter au serveur distant. Veuillez réessayer ultérieurement." ,
                    null ,
                    MessageBoxButtons.OK);                    

            };

            Action<Task<bool>> onSuccess = t =>
            {
                if (t.Result == false)
                    onErr(t);
                else
                {                    
                    Program.Settings.ClientInfo = clInfo;
                    ClientInfoChanged?.Invoke();
                    dlg.Dispose();
                }
            };

            var task = new Task<bool>(postMessage , TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);
            task.Start();
            dlg.ShowDialog();
         
        }

        //handlers:
        private void Save_Click(object sender , EventArgs e)
        {
            if (!DataChanged())
                return;

            string email = m_tbEmail.Text.Trim();
            string phone = m_tbPhone.Text.Trim();

            if ((email.Length > 0 && !ValidateEMail(email)) || (phone.Length > 0 && !ValidatePhoneNumber(phone)))
            { 
                MessageBox.Show("Veuillez fournir un numéro de téléphone ou/et une adresse e-mail valide."
                    , null);
                return;
            }

            PostSetInfoMessage();
        }

        private void Input_TextChanged(object sender , EventArgs e) => UpdateUI();
    }
}
