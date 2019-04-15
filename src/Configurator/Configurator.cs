using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Microsoft.Win32.TaskScheduler;
using Microsoft.Win32;

namespace Configurator
{
    public partial class Configurator : Form
    {
        public bool isRunning()
        {
            Process[] pname = Process.GetProcessesByName(m_programName);
            return pname.Length > 0;
        }
        public bool isStartOnBoot()
        {
            if (chkStartup.Checked)
            {

                using (RegistryKey key = Registry.LocalMachine
                    .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false))
                {
                    return key.GetValue(m_programName) != null;
                }
            }
            else
            {
                using (TaskService ts = new TaskService())
                {
                    Microsoft.Win32.TaskScheduler.Task task = ts.FindTask(m_programName, false);
                    return task != null;
                }
            }
        }
        public void setLayout()
        {
            if (running_button.Enabled)
            {
                if (isRunning())
                {
                    this.running_text.Text = "Running: Yes";
                    this.running_button.Text = "Stop";
                }
                else
                {
                    this.running_text.Text = "Running: No";
                    this.running_button.Text = "Run";
                }
            }
            if (isStartOnBoot())
            {
                this.start_on_boot_text.Text = "Start on boot: Yes";
                this.start_on_boot_button.Text = "Disable";
            }
            else
            {
                this.start_on_boot_text.Text = "Start on boot: No";
                this.start_on_boot_button.Text = "Enable";
            }
        }
        public Configurator()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Configurator_Load(object sender, EventArgs e)
        {
            txtProgram.Text = System.Configuration.ConfigurationManager.AppSettings["program"] ?? "ListaryDesktop";
            setLayout();
        }
        private void running_button_Click(object sender, EventArgs e)
        {
            m_programName = txtProgram.Text.Trim();
            running_button.Enabled = false;
            if (isRunning())
            {
                Process[] processes = Process.GetProcessesByName(m_programName);
                for (int i = 0; i < processes.Length; i++)
                {
                    try
                    {
                        processes[i].Kill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to terminate ListaryDesktop.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (!File.Exists(m_programName + ".exe"))
                {
                    MessageBox.Show(m_programName + ".exe not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    running_button.Enabled = true;
                    return;
                }
                try
                {
                    Process.Start(m_programName + ".exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Process start failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    running_button.Enabled = true;
                    return;
                }
            }
            StartThread();
        }
        private void layout_refresher_Tick(object sender, EventArgs e)
        {
            setLayout();
        }
        private void start_on_boot_button_Click(object sender, EventArgs e)
        {
            m_programName = txtProgram.Text.Trim();
            if (isStartOnBoot())
            {
                DisableBoot();
            }
            else
            {
                var fileName = GetFirstProgress();
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(fileName))
                    dir = Path.GetDirectoryName(fileName);
                var exePath = Path.Combine(dir, m_programName + ".exe");
                if (!File.Exists(exePath))
                {
                    MessageBox.Show(m_programName + ".exe not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                EnableBoot(exePath);
            }
            setLayout();
        }
        string GetFirstProgress()
        {
            Process[] processes = Process.GetProcessesByName(m_programName);
            if (processes.Count() > 0)
                return processes[0].MainModule.FileName;
            return null;
        }
        private string m_programName = "ListaryDesktop";
        Thread m_runThread;
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            m_programName = txtProgram.Text.Trim();
            StopThread();
            StartThread();
        }
        void StopThread()
        {
            try
            {
                if (m_runThread != null)
                    m_runThread.Abort();
            }
            catch (Exception)
            {
            }
        }
        void StartThread()
        {
            m_runThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate ()
                {
                    running_button.Enabled = true;
                });
                setLayout();
            });
            m_runThread.Start();
        }
        void EnableBoot(string path)
        {
            if (chkStartup.Checked)
                EnableRgistryKey(path);
            else
                EnableTask(path);
        }
        void DisableBoot()
        {
            if (chkStartup.Checked)
                DisableRgistryKey();
            else
                DisableTask();
        }
        void EnableRgistryKey(string path)
        {
            //打开注册表子项
            using (RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                //增加开机启动项
                key.SetValue(m_programName, string.Format("\"{0}\"", path));
            }
        }
        void DisableRgistryKey()
        {
            //打开注册表子项
            using (RegistryKey key = Registry.LocalMachine
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(m_programName);
            }
        }
        void EnableTask(string path)
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = m_programName;
                td.Triggers.Add(new LogonTrigger());//new BootTrigger()
                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;
                td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                td.Settings.StartWhenAvailable = true;
                td.Principal.RunLevel = TaskRunLevel.Highest;
                //td.Principal.LogonType = TaskLogonType.S4U;
                td.Actions.Add(new ExecAction(string.Format("\"{0}\"", path), "", null));
                ts.RootFolder.RegisterTaskDefinition(m_programName, td);
            }
        }
        void DisableTask()
        {
            using (TaskService ts = new TaskService())
            {
                Microsoft.Win32.TaskScheduler.Task task = ts.FindTask(m_programName);
                if (task != null)
                {
                    ts.RootFolder.DeleteTask(m_programName);
                }
            }
        }
    }
}
