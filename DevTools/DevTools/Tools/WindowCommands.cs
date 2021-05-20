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
            int y = -300;
            int height = (windowWidth / 2) - 15; // Divide window size by 2 and subtract task bar height
            MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        // Vertical orientation monitor
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        public static void ResizeWindowRightHalfBottomVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            string programName = GetDescription(targetWindow);
            int y = 965;
            int height = (windowWidth / 2) - 15; // Divide window size by 2 and subtract task bar height
            MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        public static void ResizeWindowCenterFullHorizontal(Programs targetWindow, int horizontalWidth)
        {
            string programName = GetDescription(targetWindow);
            int y = 0;
            int width = 1;
            int height = 1;
            MoveWindow(programName, horizontalWidth, y, width, height);
            MaximizeWindow(programName);
        }

        public static void ResizeWindowLeftMaximizedVertical(Programs targetWindow, int horizontalWidth)
        {
            string programName = GetDescription(targetWindow);
            int x = horizontalWidth - 1;
            int y = 0;
            int width = 1;
            int height = 1;
            MoveWindow(programName, x, y, width, height);
            MaximizeWindow(programName);
        }

        private static void MoveWindow(string programName, int x, int y, int width, int height, bool repaint = true)
        {
            Process process = Process.GetProcessesByName(programName)[0];
            IntPtr handle = process.MainWindowHandle;
            WindowPositioning windowPositioning = new WindowPositioning();
            if (GetWindowRect(handle, ref windowPositioning))
            {
                MoveWindow(handle, x, y, width, height, repaint);
            }
        }

        private static void MaximizeWindow(string programName)
        {
            Process process = Process.GetProcessesByName(programName)[0];
            IntPtr handle = process.MainWindowHandle;
            WindowPositioning windowPositioning = new WindowPositioning();
            if (GetWindowRect(handle, ref windowPositioning))
            {
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
