using System.Diagnostics;
using System.Runtime.InteropServices;

public class WindowInfo
{
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static Rectangle? GetAoE2WindowBounds()
    {
        foreach (var proc in Process.GetProcessesByName("AoE2DE_s")) // 実行ファイル名（拡張子なし）
        {
            if (proc.MainWindowHandle != IntPtr.Zero && IsWindowVisible(proc.MainWindowHandle))
            {
                if (GetWindowRect(proc.MainWindowHandle, out RECT rect))
                {
                    return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                }
            }
        }

        return null; // ウィンドウが見つからなかった
    }
}
