using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AOE2DETOOL.Utilities
{
    public class WindowActivator
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_RESTORE = 9;

        public static bool FocusWindowByTitle(string windowTitle)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowHandle != IntPtr.Zero &&
                    proc.MainWindowTitle.Contains(windowTitle, StringComparison.OrdinalIgnoreCase))
                {
                    // 最小化されていたら復元
                    ShowWindow(proc.MainWindowHandle, SW_RESTORE);
                    return SetForegroundWindow(proc.MainWindowHandle);
                }
            }

            return false; // 該当ウィンドウが見つからなかった
        }
    }
}
