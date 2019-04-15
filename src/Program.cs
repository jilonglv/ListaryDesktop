using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using WinForms = System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;
using System.IO;
using AutoHotkey.Interop;
using System.Collections.Generic;

namespace ListaryDesktop
{
    static class Program
    {
        static AutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;
        static ConcurrentDictionary<string, string> m_hotkeyIndex = new ConcurrentDictionary<string, string>();
        [STAThread]
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            bool isSuccess = false;
            var hook = new KeyboardHook();
            try
            {
                LoadConfig();
                hook.OnKeyUpEvent += Hook_OnKeyUpEvent;
                hook.OnKeyPressEvent += Hook_OnKeyPressEvent;
                isSuccess = hook.SetHook();
            }
            catch
            {
            }
            if (isSuccess)
                Application.Run();
            else
                MessageBox.Show("Start faield!");
            hook.UnHook();
        }
        private static void Hook_OnKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar.ToString());
        }
        private static void Hook_OnKeyUpEvent(object sender, KeyEventArgs e)
        {
            var keyName = HotKeyInfo.GetStringByKey(e);
            if (m_hotkeyIndex.ContainsKey(keyName))
            {
                var cmd = m_hotkeyIndex[keyName];
                ahk.ExecRaw(cmd);
                //return (IntPtr)1;
            }
            //Console.WriteLine(e.KeyData.ToString());
        }
        static void LoadConfig()
        {
            foreach (var line in File.ReadAllLines("keys"))
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var keyLines = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyLines.Length < 2)
                    continue;
                var keyName = keyLines[0].Trim().Replace(" ", "");
                m_hotkeyIndex[keyName] = keyLines[1].Trim();
            }
        }
    }

    internal class InterceptKeys
    {
        #region Delegates

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion
        //Hook结构 
        [StructLayout(LayoutKind.Sequential)]
        public class HookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;
        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }
        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(HookType.WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType idHook,
                                                      LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                                                   IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
    }
}
