using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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

        public static void RunCommands(string terminal, string[] commands)
        {
            StringBuilder commandsPipe = new StringBuilder();
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
