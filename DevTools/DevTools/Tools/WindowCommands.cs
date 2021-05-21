using System;   
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevTools.Tools
{
    static class WindowCommands
    {

        public static bool CloseWindow(Programs targetWindow)
        {
            try
            {
                var programName = ProgramsTool.GetDescription(targetWindow);
                var processes = Process.GetProcessesByName(programName);
                foreach (var process in processes)
                {
                    process.Kill();
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close process: " + e);
            }
            return false;
        }

        // Vertical orientation monitor
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        public static bool ResizeWindowRightHalfTopVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            string programName = ProgramsTool.GetDescription(targetWindow);
            int y = -300;
            int height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        // Vertical orientation monitor
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        public static bool ResizeWindowRightHalfBottomVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            string programName = ProgramsTool.GetDescription(targetWindow);
            int y = 965;
            int height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        public static bool ResizeWindowCenterFullHorizontal(Programs targetWindow)
        {
            string programName = ProgramsTool.GetDescription(targetWindow);
            int x = -1;
            int y = 0;
            int width = 1;
            int height = 1;
            var result = MoveWindow(programName, x, y, width, height) && MaximizeWindow(programName);
            return result;
        }

        public static bool ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            string programName = ProgramsTool.GetDescription(targetWindow);
            int x = -2000;
            int y = 0;
            int width = 1;
            int height = 1;
            var result = MoveWindow(programName, x, y, width, height) && MaximizeWindow(programName);
            return result;
        }

        private static bool MoveWindow(string programName, int x, int y, int width, int height, bool repaint = true)
        {
            try
            {
                var processes = Process.GetProcessesByName(programName);
                foreach (var process in processes)
                {
                    IntPtr handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        WindowPositioning windowPositioning = new WindowPositioning();
                        if (GetWindowRect(handle, ref windowPositioning))
                        {
                            MoveWindow(handle, x, y, width, height, repaint);
                            return true;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Failed to find process: " + e);
            }
            return false;
        }


        private static bool MaximizeWindow(string programName)
        {
            try
            {
                var processes = Process.GetProcessesByName(programName);
                foreach (var process in processes)
                {
                    IntPtr handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        WindowPositioning windowPositioning = new WindowPositioning();
                        if (GetWindowRect(handle, ref windowPositioning))
                        {
                            ShowWindow(handle, SW_SHOWMAXIMIZED);

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to find process: " + e);
            }
            return false;
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
    }
}
