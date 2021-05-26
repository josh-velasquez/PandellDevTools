using System;
using System.Runtime.InteropServices;

namespace DevTools.Tools
{
    static class MouseCommands
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        /// <summary>
        /// Moves the cursor and clicks on the target location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveAndClick(int x, int y)
        {
            SetCursorPosition(x, y);
            SendLeftClick();
        }

        /// <summary>
        /// Sets the cursor position to the target position
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void SetCursorPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }

        /// <summary>
        /// Sends left click action to the current location of the cursor
        /// </summary>
        public static void SendLeftClick()
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
