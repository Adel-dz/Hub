using DGD.Hub.DLG;
using DGD.HubCore.DLG;
using easyLib.Extensions;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.Hub
{
    sealed partial class MainWindow: Form
    {
        IView m_curView;
        Log.FlashBox m_flashBox;

        public MainWindow()
        {
            InitializeComponent();

            CurrentView = InitAboutView();
            InitSpotView();
            InitHelpView();
            InitJetSkiesView();
            InitMachineriesView();
            InitQuadView();
            InitRangeValuesView();
            InitSettingsView();

            Log.LogEngin.MessageReady += LogEngin_MessageReady;
            Log.LogEngin.MessageTimeout += LogEngin_MessageTimeout;

            Application.Idle += Application_Idle;


            if (Program.Settings.IsMaximized)
                WindowState = FormWindowState.Maximized;
            else
            {
                Rectangle rc = Program.Settings.FrameRectangle;

                if (!rc.IsEmpty)
                {
                    StartPosition = FormStartPosition.Manual;
                    Bounds = rc;
                }
            }
        }


        //protected
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (m_flashBox != null)
                m_flashBox.AutoMove();

            base.OnResize(e);
        }

        protected override void OnMove(EventArgs e)
        {
            if (m_flashBox != null)
                m_flashBox.AutoMove();

            base.OnMove(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (WindowState == FormWindowState.Maximized)
                Program.Settings.IsMaximized = true;
            else
            {
                Program.Settings.IsMaximized = false;
                Program.Settings.FrameRectangle = Bounds;
            }
        }

        //private:
        IView CurrentView
        {
            get { return m_curView; }

            set
            {
                if (m_curView != null)
                {
                    foreach (ToolStripItem item in m_mainToolStrip.Items)
                        if (item.Tag == m_curView)
                        {
                            (item as ToolStripButton).Checked = false;
                            break;
                        }

                    m_curView.Deactivate(m_viewsPanel);
                }

                foreach (ToolStripItem item in m_mainToolStrip.Items)
                    if (item.Tag == value)
                    {
                        (item as ToolStripButton).Checked = true;
                        break;
                    }

                m_curView = value;
                m_curView.Activate(m_viewsPanel);
            }
        }

        void EndFlashBox()
        {
            if (m_flashBox != null)
            {
                m_flashBox.Dispose();
                m_flashBox = null;
            }
        }

        void StartFlashBox(string msg)
        {
            if (m_flashBox != null)
                EndFlashBox();

            m_flashBox = new Log.FlashBox(msg);
            m_flashBox.Show(this);
            //Activate();
        }

        SpotView.SpotView InitSpotView()
        {
            var view = new SpotView.SpotView();
            m_tsbSpotView.Tag = view;

            return view;
        }

        AboutView.AboutView InitAboutView()
        {
            var view = new AboutView.AboutView();
            m_tsbAbout.Tag = view;

            return view;
        }

        DummyView.DummyView InitDummyView(ToolStripButton btn)
        {
            var view = new DummyView.DummyView();
            btn.Tag = view;

            return view;
        }

        DummyView.DummyView InitRangeValuesView() => InitDummyView(m_tsbRangeValue);
        DummyView.DummyView InitMachineriesView() => InitDummyView(m_tsbMachinery);
        DummyView.DummyView InitQuadView() => InitDummyView(m_tsbQuad);
        DummyView.DummyView InitJetSkiesView() => InitDummyView(m_tsbJetSki);
        DummyView.DummyView InitSettingsView() => InitDummyView(m_tsbSettings);
        DummyView.DummyView InitHelpView() => InitDummyView(m_tsbHelp);

        T RunTask<T>(Func<T> func , bool useThreadPool = false)
        {
            T result = default(T);

            Action<Task<T>> onSuccess = t => result = t.Result;

            Action<Task> onErr = t =>
            {
                throw t.Exception.InnerException;
            };


            var task = new Task<T>(func , useThreadPool ? TaskCreationOptions.LongRunning : TaskCreationOptions.LongRunning);
            task.OnSuccess(onSuccess);
            task.OnError(onErr);
            task.Start();

            return result;
        }

        //handlers:
        void Application_Idle(object sender , EventArgs e)
        {
            Application.Idle -= Application_Idle;

            Program.DialogManager.Start();
        }

        private void SetView_Handler(object sender , EventArgs e)
        {
            try
            {
                CurrentView = (sender as ToolStripButton).Tag as IView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this , ex.Message , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }
            
        private void LogEngin_MessageTimeout()
        {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(EndFlashBox));
            else
                EndFlashBox();
        }

        private void LogEngin_MessageReady(string msg)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string>(StartFlashBox) , msg);
            else
                StartFlashBox(msg);
        }

        private void Quit_Click(object sender , EventArgs e)
        {
            Application.Exit();
        }
    }
}
