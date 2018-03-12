using DGD.HubCore.DLG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;



namespace DGD.Hub.DLG
{
    sealed partial class ProfileDialog: Form
    {
        public ProfileDialog(IEnumerable<ProfileInfo> profiles)
        {
            Assert(profiles != null);
            Assert(profiles.Any() == true);

            InitializeComponent();

            m_cbProfiles.Items.AddRange(profiles.ToArray());
            m_cbProfiles.DisplayMember = "ProfileName";
            m_cbProfiles.SelectedIndex = 0;

            const string profileInfo = "Sélectionnez votre profil. Si celui-ci est introuvable, " +
             "ou bien si vous n’êtes pas sûr de vous, contactez la sous-direction de la valeur.";
            const string contactInfo = "Veuillez saisir votre nom. " +
                "Cette information permettra de vous identifier en cas de besoin.";
            const string phoneInfo = "Saisissez votre numéro de téléphone." +
                "Vous pouvez omettre cette information si vous fournissez votre e-mail.";
            const string emailInfo = "Saisissez votre adresse e-mail. " +
                "Vous pouvez omettre cette information si vous fournissez votre numéro de téléphone.";

            EventHandler leaveHandler = delegate { m_lblInfo.Text = ""; };

            m_cbProfiles.Enter += delegate { m_lblInfo.Text = profileInfo; };
            m_cbProfiles.Leave += leaveHandler;

            m_tbContact.Enter += delegate { m_lblInfo.Text = contactInfo; };
            m_tbContact.Leave += leaveHandler;

            m_tbPhone.Enter += delegate { m_lblInfo.Text = phoneInfo; };
            m_tbPhone.Leave += leaveHandler;

            m_tbEMail.Enter += delegate { m_lblInfo.Text = emailInfo; };
            m_tbEMail.Leave += leaveHandler;
        }


        public ProfileInfo SelectedProfile => m_cbProfiles.SelectedItem as ProfileInfo;
        public string Contact => m_tbContact.Text.Trim();
        public string ContactPhone
        {
            get
            {
                string phone = m_tbPhone.Text.Trim();
                return ValidatePhoneNumber(phone) ? phone : "";
            }
        }

        public string ContactEMail
        {
            get
            {
                string email = m_tbEMail.Text.Trim();
                return ValidateEMail(email) ? email : "";
            }
        }

                       

        //private:
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


        //handlers:
        private void OK_Click(object sender , EventArgs e)
        {
            DialogResult = DialogResult.None;

            if (Contact == "")
                MessageBox.Show("Vous devez impérativement saisir votre nom." , null);
            else if (ContactPhone == "" && ContactEMail == "")
                MessageBox.Show("Veuillez fournir un numéro de téléphone ou/et une adresse e-mail valide." , null);
            else
                DialogResult = DialogResult.OK;
                
        }
    }
}
