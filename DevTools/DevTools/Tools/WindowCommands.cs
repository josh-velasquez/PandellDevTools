using System;   
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevTools.Tools
{
    enum AppStatus
    {
        Launch,
        Close,
        Resize
    }
    static class WindowCommands
    {
        const int defaultWindowHeight = 10;
        const int defaultWindowWidth = 10;

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

        public static bool ResizeWindowLeftHalfTopVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var y = -300;
            var x = -2000;
            var height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, x, y, windowWidth, height);
        }

        public static bool ResizeWindowLeftHalfBottomVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var y = 965;
            var x = -2000;
            var height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, x, y, windowWidth, height);
        }

        /// <summary>
        /// Vertical oriented monitor. The total height for task bar is 30 pixels so if you have two windows on one (vertical orientation) then 30/2 = 15
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        /// <returns></returns>
        public static bool ResizeWindowRightHalfTopVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var y = -300;
            var height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        /// <summary>
        /// Vertical oriented monitor. The total height for task bar is 30 pixels so if you have two windows on one (vertical orientation) then 30/2 = 15
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        /// <returns></returns>
        public static bool ResizeWindowRightHalfBottomVertical(Programs targetWindow, int windowHeight, int windowWidth)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var y = 965;
            var height = (windowHeight / 2) - 15; // Divide window size by 2 and subtract task bar height
            return MoveWindow(programName, windowHeight, y, windowWidth, height);
        }

        /// <summary>
        /// Maximizes the window to the center of the monitor
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <returns></returns>
        public static bool ResizeWindowCenterMaximizedHorizontal(Programs targetWindow)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var x = -1;
            var y = 0;
            var width = defaultWindowWidth;
            var height = defaultWindowHeight;
            var result = MoveWindow(programName, x, y, width, height) && MaximizeWindow(programName);
            return result;
        }

        /// <summary>
        /// Maxemizes the window to the left monitor
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <returns></returns>
        public static bool ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            var programName = ProgramsTool.GetDescription(targetWindow);
            var x = -2000;
            var y = 0;
            var width = defaultWindowWidth;
            var height = defaultWindowHeight;
            var result = MoveWindow(programName, x, y, width, height) && MaximizeWindow(programName);
            return result;
        }

        /// <summary>
        /// Moves window anywhere on the screen
        /// </summary>
        /// <param name="programName">Name of the application to move</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="width">Width of the application when moved</param>
        /// <param name="height">Height of the application when moved</param>
        /// <param name="repaint">true if moved; false otherwise</param>
        /// <returns></returns>
        private static bool MoveWindow(string programName, int x, int y, int width, int height, bool repaint = true)
        {
            try
            {
                var processes = Process.GetProcessesByName(programName);
                foreach (var process in processes)
                {
                    var handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        var windowPositioning = new WindowPositioning();
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

        /// <summary>
        /// Maximizes the window
        /// </summary>
        /// <param name="programName">Target window to maximiz</param>
        /// <returns>true if maximized; false otherwise</returns>
        private static bool MaximizeWindow(string programName)
        {
            try
            {
                var processes = Process.GetProcessesByName(programName);
                foreach (var process in processes)
                {
                    var handle = process.MainWindowHandle;
                    if (handle != IntPtr.Zero)
                    {
                        var windowPositioning = new WindowPositioning();
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

        // Maximizing window commands
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int showWindow);
    }
}
