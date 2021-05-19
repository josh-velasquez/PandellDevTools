using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DevTools.Tools
{
    static class WindowCommands
    {
        // Vertical orientation monitor
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        public static void ResizeWindowRightHalfTopVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            string programName = GetDescription(targetWindow);
            Process process = Process.GetProcessesByName(programName)[0];
            IntPtr handle = process.MainWindowHandle;
            WindowPositioning windowPositioning = new WindowPositioning();
            if (GetWindowRect(handle, ref windowPositioning))
            {
                MoveWindow(handle, windowHeight, -300, windowWidth, (windowHeight / 2) - 15, true);
            }
        }

        // Vertical orientation monitor
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        public static void ResizeWindowRightHalfBottomVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            string programName = GetDescription(targetWindow);
            Process process = Process.GetProcessesByName(programName)[0];
            IntPtr handle = process.MainWindowHandle;
            WindowPositioning windowPositioning = new WindowPositioning();
            if (GetWindowRect(handle, ref windowPositioning))
            {
                MoveWindow(handle, windowHeight, 965, windowWidth, (windowWidth / 2) - 15, true);
            }
        }

        public static void ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            string programName = GetDescription(targetWindow);
            Process process = Process.GetProcessesByName(programName)[0];
            IntPtr handle = process.MainWindowHandle;
            WindowPositioning windowPositioning = new WindowPositioning();
            if (GetWindowRect(handle, ref windowPositioning))
            {
                // Move to the left
                MoveWindow(handle, -2000, 0, windowPositioning.right, windowPositioning.top, true);
                // Maximize window
                ShowWindow(handle, SW_SHOWMAXIMIZED);
            }
        }

        // Moving and resizing window
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref WindowPositioning Rect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowPositioning
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        // Maximizing window
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int showWindow);

        /// <summary>
        /// Gets the description of enum
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Description of enum</returns>
        private static string GetDescription(object e)
        {
            return e
            .GetType()
            .GetMember(e.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description
        ?? e.ToString();
        }
    }
}
