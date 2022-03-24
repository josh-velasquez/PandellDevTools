using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DevTools.Tools
{
    static class ProcessCommands
    {
        /// <summary>
        /// Runs single command
        /// </summary>
        /// <param name="command"></param>
        public static bool RunCommand(string command)
        {
            try
            {
                Process.Start(command);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run command: " + command + "\nError: " + e);
            }
            return false;
        }

        /// <summary>
        /// Runs command with single argument
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public static bool RunCommand(string command, string args)
        {
            try
            {
                Process.Start(command, args);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run command: " + command + "\nError: " + e);
            }
            return false;
        }

        /// <summary>
        /// Runs command with multiple arguments
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="commands"></param>
        public static void RunCommands(string terminal, string[] commands)
        {
            var commandsPipe = new StringBuilder();
            try
            {
                for (int i = 0; i < commands.Length - 1; i++)
                {
                    commandsPipe.Append(commands[i] + " && ");
                }
                commandsPipe.Append(commands[commands.Length - 1]);
                Process.Start(terminal, commandsPipe.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run commands.\nError: " + e);
            }
        }

        /// <summary>
        /// Executes commands on the windows terminal
        /// </summary>
        /// <param name="windowsTerminalPath"></param>
        /// <param name="command"></param>
        public static void RunCommandWindowsTerminal(string windowsTerminalPath, string command)
        {
            try
            {
                Process.Start(windowsTerminalPath, command);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run command: " + command + "\nError: " + e);
            }
        }

        /// <summary>
        /// Delete target path directory
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                else
                    throw new DirectoryNotFoundException();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to delete directory: " + path + "\nError: " + e);
            }
        }
    }
}
