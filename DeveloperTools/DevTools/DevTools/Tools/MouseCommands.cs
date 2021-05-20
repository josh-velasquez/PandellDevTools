using System;
using System.Runtime.InteropServices;

namespace DevTools.Tools
{
    static class MouseCommands
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        public static void MoveAndClick(int x, int y)
        {
            SetCursorPosition(x, y);
            SendClick();
        }
        public static void SetCursorPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }
        public static void SendClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new IntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new IntPtr());
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(
               UInt32 dwFlags, // motion and click options
               UInt32 dx, // horizontal position or change
               UInt32 dy, // vertical position or change
               UInt32 dwData, // wheel movement
               IntPtr dwExtraInfo // application-defined information
        );
    }
}
