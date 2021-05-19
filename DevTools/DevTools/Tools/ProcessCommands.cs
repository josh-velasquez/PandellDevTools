using System;
using System.Diagnostics;
using System.IO;

namespace DevTools.Tools
{
    static class ProcessCommands
    {
        public static void RunCommand(string command)
        {
            try
            {
                Process.Start(command);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run command: " + command + "\nError: " + e);
            }
        }

        public static void RunCommand(string command, string args)
        {
            try
            {
                Process.Start(command, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to run command: " + command + "\nError: " + e);
            }
        }

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
