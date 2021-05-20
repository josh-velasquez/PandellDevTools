using DevTools.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string SLACK_PATH = @"C:\Users\joshv\AppData\Local\slack\slack.exe";
        const string OUTLOOK_PATH = @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE";
        const string SPOTIFY_PATH = @"C:\Users\joshv\AppData\Roaming\Spotify\Spotify.exe";
        const string WINDOWSTERMINAL_PATH = @"C:\Users\joshv\AppData\Local\Microsoft\WindowsApps\wt.exe";
        const string CHROME_PATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        IDictionary<string, string> WEBSITES = new Dictionary<string, string>(){
            {"GitHub", "https://github.com/pandell/LandRiteWeb"},
            {"TeamCity", "https://build.pandell.com/"},
            {"LandRiteJira", "https://pandell.atlassian.net/secure/RapidBoard.jspa?rapidView=87"},
            {"Harvest", "https://pandell.harvestapp.com/welcome"},
            { "EmployeePortal", "http://employee/"}};

        int HORIZONTAL_SCREEN_WIDTH = (int)SystemParameters.PrimaryScreenWidth;
        int HORIZONTAL_SCREEN_HEIGHT = (int)SystemParameters.PrimaryScreenHeight;
        public MainWindow()
        {
            InitializeComponent();
            var repoSourceDirectory = @"D:\Repository\LandRiteWeb";
            SrcDirTextBox.Text = repoSourceDirectory;
        }

        private void OnCreatePrClick(object sender, RoutedEventArgs e)
        {
            PrGenerator prGenerator = new PrGenerator();
            prGenerator.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            prGenerator.Show();
        }

        private void OnClobberClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] commands = { @"cd D:\Repository\PandellDevTools", @"mkdir testing" };
                ProcessCommands.RunCommands("", commands);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex);
            }
            //StatusListBox.Items.Add("Clobbering files...");
            //var targetRepoPath = Path.Combine(SrcDirTextBox.Text);
            //UpdateStatus("Clobbering files...", false);
            //try
            //{
            //    ProcessCommands.RunCommand(targetRepoPath);
            //    UpdateStatus("Dev tool started.", true, true);
            //}
            //catch (Exception ex)
            //{
            //    UpdateStatus("Dev tool started.", true, true);
            //    Debug.WriteLine("Error: " + ex);
            //}
        }

        private void OnBuildStartServerClick(object sender, RoutedEventArgs e)
        {
            StatusListBox.Items.Add("Building server...");
            StatusListBox.Items.Add("Server built.");
            StatusListBox.Items.Add("Starting server...");
            StatusListBox.Items.Add("Server started.");
        }

        private void OnLaunchDevToolClick(object sender, RoutedEventArgs e)
        {
            var devToolPath = Path.Combine(SrcDirTextBox.Text, "tools", "LR_Start-DevTool.ps1");
            UpdateStatus("Launching Dev Tool...", false);
            try
            {
                ProcessCommands.RunCommand(devToolPath);
                UpdateStatus("Dev tool started.", true, true);
            }
            catch (Exception ex)
            {
                UpdateStatus("Dev tool started.", true, true);
                Debug.WriteLine("Error: " + ex);
            }
        }

        private bool ConnectToVpn()
        {
            try
            {
                // wait for 2 seconds
                Thread.Sleep(2000);

                // Move and to tray icon
                MouseCommands.MoveAndClick(2351, (1440 - 16));
                Thread.Sleep(500);

                // Move and click to connect icon
                MouseCommands.MoveAndClick(2401, (1440 - 77));
                Thread.Sleep(10000);

                // close tray
                MouseCommands.MoveAndClick(2351, (1440 - 16));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchSlack()
        {
            try
            {
                ProcessCommands.RunCommand(SLACK_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchOutlook()
        {
            try
            {
                ProcessCommands.RunCommand(OUTLOOK_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchChromeWebsites()
        {
            try
            {
                foreach (var website in WEBSITES)
                {
                    ProcessCommands.RunCommand(CHROME_PATH, website.Value);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchSpotify()
        {
            try
            {
                ProcessCommands.RunCommand(SPOTIFY_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchWindowsTerminal()
        {
            try
            {
                ProcessCommands.RunCommand(WINDOWSTERMINAL_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool ResizeWindowRightHalfTopVertical(Programs targetWindow)
        {
            try
            {
                WindowCommands.ResizeWindowRightHalfTopVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool ResizeWindowCenterFullHorizontal(Programs targetWindow)
        {
            try
            {
                WindowCommands.ResizeWindowCenterFullHorizontal(targetWindow, HORIZONTAL_SCREEN_WIDTH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool ResizeWindowRightHalfBottomVertical(Programs targetWindow)
        {
            try
            {
                WindowCommands.ResizeWindowRightHalfBottomVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            try
            {
                WindowCommands.ResizeWindowLeftMaximizedVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private void UpdateStatus(string message, bool commandUpdate, bool success = false)
        {
            if (commandUpdate)
            {
                if (success)
                    StatusListBox.Items.Add(new ListBoxItem { Content = message, Background = Brushes.LightGreen });
                else
                    StatusListBox.Items.Add(new ListBoxItem { Content = message, Background = Brushes.Red });
            }
            else
            {
                StatusListBox.Items.Add(new ListBoxItem { Content = message, Background = Brushes.Gray });
            }
        }

        private void OnLaunchClick(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Launching programs...", false);

            // Connect to vpn
            UpdateStatus("Connecting to VPN...", false);
            var vpnConnected = ConnectToVpn();
            if (vpnConnected)
                UpdateStatus("Connected to VPN.", true, true);
            else
                UpdateStatus("Failed to connect to VPN.", true, false);

            // Launch slack
            UpdateStatus("Launching Slack...", false);
            var slackLaunched = LaunchSlack();
            if (slackLaunched)
                UpdateStatus("Slack launched.", true, true);
            else
                UpdateStatus("Slack failed to launch.", true, false);

            // Launch outlook
            UpdateStatus("Launching Outlook...", false);
            var outlookLaunched = LaunchOutlook();
            if (outlookLaunched)
                UpdateStatus("Outlook launched.", true, true);
            else
                UpdateStatus("Outlook failed to launch.", true, false);

            // Launch chrome website(s)
            var websitesLaunched = LaunchChromeWebsites();
            if (websitesLaunched)
                UpdateStatus("All website(s) launched.", true, true);
            else
                UpdateStatus("Failed to launch website(s).", true, false);

            // Launch spotify
            var spotifyLaunched = LaunchSpotify();
            if (spotifyLaunched)
                UpdateStatus("Spotify launched.", true, true);
            else
                UpdateStatus("Spotify failed to launch.", true, false);

            // Launch terminal
            var terminalLaunched = LaunchWindowsTerminal();
            if (terminalLaunched)
                UpdateStatus("Windows terminal launched.", true, true);
            else
                UpdateStatus("Windows terminal failed to launch.", true, false);

            // Resizing Slack window
            var resizedSlack = ResizeWindowRightHalfTopVertical(Programs.Slack);
            if (resizedSlack)
                UpdateStatus("Slack window resized.", true, true);
            else
                UpdateStatus("Failed to resize Slack.", true, false);

            // Resizing Outlook window
            var resizedOutlook = ResizeWindowRightHalfBottomVertical(Programs.Outlook);
            if (resizedOutlook)
                UpdateStatus("Outlook window resized.", true, true);
            else
                UpdateStatus("Failed to resize Outlook.", true, false);

            // Resizing Chrome window
            var resizedChrome = ResizeWindowCenterFullHorizontal(Programs.Chrome);
            if (resizedChrome)
                UpdateStatus("Chrome window resized.", true, true);
            else
                UpdateStatus("Failed to resize Chrome.", true, false);

            // Resizing Spotify window
            var resizedSpotify = ResizeWindowLeftMaximizedVertical(Programs.Spotify);
            if (resizedSpotify)
                UpdateStatus("Spotify window resized.", true, true);
            else
                UpdateStatus("Failed to resize Spotify.", true, false);

        }

        private void OnDelTempAspFilesClick(object sender, RoutedEventArgs e)
        {
            var tempFiles = @"C:\Users\joshv\AppData\Local\Temp\Temporary ASP.NET Files";
            UpdateStatus("Deleting Temporary ASP.NET Files...", false);
            try
            {
                ProcessCommands.DeleteDirectory(tempFiles);
                UpdateStatus("Temporary ASP.NET Files deleted.", true, true);
            }
            catch (Exception ex)
            {
                UpdateStatus("Failed to delete Temporary ASP.NET Files.", true, false);
                Debug.WriteLine("Error: " + ex);
            }
        }

        private void OnFixWindowsClick(object sender, RoutedEventArgs e)
        {
            // Resizing Slack window
            var resizedSlack = ResizeWindowRightHalfTopVertical(Programs.Slack);
            if (resizedSlack)
                UpdateStatus("Slack window resized.", true, true);
            else
                UpdateStatus("Failed to resize Slack.", true, false);

            // Resizing Outlook window
            var resizedOutlook = ResizeWindowRightHalfBottomVertical(Programs.Outlook);
            if (resizedOutlook)
                UpdateStatus("Outlook window resized.", true, true);
            else
                UpdateStatus("Failed to resize Outlook.", true, false);

            // Resizing Chrome window
            var resizedChrome = ResizeWindowLeftMaximizedVertical(Programs.Chrome);
            if (resizedChrome)
                UpdateStatus("Chrome window resized.", true, true);
            else
                UpdateStatus("Failed to resize Chrome.", true, false);
        }

        private void OnHomeTimeClick(object sender, RoutedEventArgs e)
        {
            // Close programs
        }
    }
}
