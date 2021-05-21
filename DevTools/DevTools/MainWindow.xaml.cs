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
        const string TEMP_ASP_FILES = @"C:\Users\joshv\AppData\Local\Temp\Temporary ASP.NET Files";
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
            UpdateStatus("Clobbering files...", false);
            var targetRepoPath = Path.Combine(SrcDirTextBox.Text);
            var command = "yarn clobber";



            try
            {
                //ProcessCommands.DeleteDirectory(TEM)
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex);
            }

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
            UpdateStatus("Building server...", false);
            UpdateStatus("Server built.", false);
            UpdateStatus("Starting server...", false);
            UpdateStatus("Server started.", false);
        }

        private void OnLaunchDevToolClick(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Launching Dev Tool...", false);
            var devToolPath = Path.Combine(SrcDirTextBox.Text, "tools", "LR_Start-DevTool.ps1");
            try
            {
                ProcessCommands.RunCommandWindowsTerminal(WINDOWSTERMINAL_PATH, devToolPath);
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

        private bool LaunchIncognitoBrowser()
        {
            try
            {
                var args = "google.com -incognito";
                ProcessCommands.RunCommand(CHROME_PATH, args);
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
            return WindowCommands.ResizeWindowRightHalfTopVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
        }

        private bool ResizeWindowCenterFullHorizontal(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowCenterFullHorizontal(targetWindow);
        }

        private bool ResizeWindowRightHalfBottomVertical(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowRightHalfBottomVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
        }

        private bool ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowLeftMaximizedVertical(targetWindow);
        }

        private void UpdateStatus(string message, bool commandUpdate, bool success = false)
        {
            Dispatcher.Invoke(() =>
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
            });
        }

        private void OnLaunchClick(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
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
            }).Start();
        }

        private void OnDelTempAspFilesClick(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Deleting Temporary ASP.NET Files...", false);
            try
            {
                ProcessCommands.DeleteDirectory(TEMP_ASP_FILES);
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
            new Thread(() =>
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
            }).Start();
        }

        private bool CloseSlack()
        {
            try
            {
                WindowCommands.CloseWindow(Programs.Slack);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close Slack: " + e);
                return false;
            }
            return true;
        }

        private bool CloseOutlook()
        {
            try
            {
                WindowCommands.CloseWindow(Programs.Outlook);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close Outlook: " + e);
                return false;
            }
            return true;
        }

        private bool CloseChrome()
        {
            try
            {
                WindowCommands.CloseWindow(Programs.Chrome);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close Chrome: " + e);
                return false;
            }
            return true;
        }

        private bool CloseSpotify()
        {
            try
            {
                WindowCommands.CloseWindow(Programs.Spotify);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close Spotify: " + e);
                return false;
            }
            return true;
        }

        private bool CloseWindowsTerminal()
        {
            try
            {
                WindowCommands.CloseWindow(Programs.WindowsTerminal);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to close Windows Terminal: " + e);
                return false;
            }
            return true;
        }

        private void OnHomeTimeClick(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                UpdateStatus("Closing programs...", false);

                // Closing Slack
                var slackClosed = CloseSlack();
                if (slackClosed)
                    UpdateStatus("Slack closed.", true, true);
                else
                    UpdateStatus("Failed to close Slack.", true, false);

                // Closing Outlook
                var outlookClosed = CloseOutlook();
                if (outlookClosed)
                    UpdateStatus("Outlook closed.", true, true);
                else
                    UpdateStatus("Failed to close Outlook.", true, false);

                // Closing Chrome
                var chromeClosed = CloseChrome();
                if (chromeClosed)
                    UpdateStatus("Chrome closed.", true, true);
                else
                    UpdateStatus("Failed to close Chrome.", true, false);

                // Closing Spotify
                var spotifyClosed = CloseSpotify();
                if (spotifyClosed)
                    UpdateStatus("Spotify closed.", true, true);
                else
                    UpdateStatus("Failed to close Spotify.", true, false);

                // Closing Windows Terminal
                var wtClosed = CloseWindowsTerminal();
                if (wtClosed)
                    UpdateStatus("Windows Terminal closed.", true, true);
                else
                    UpdateStatus("Failed to close Windows Terminal.", true, false);
            }).Start();
        }

        private void OnIncognitoClick(object sender, RoutedEventArgs e)
        {
            var launchedIncognito = LaunchIncognitoBrowser();
            if (launchedIncognito)
                UpdateStatus("Incognito browser launched.", true, true);
            else
                UpdateStatus("Incognito browser failed to launch.", true, false);
        }
    }
}
