using DevTools.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        string SLACK_PATH = @"\Local\slack\slack.exe";
        string SPOTIFY_PATH = @"\Roaming\Spotify\Spotify.exe";
        string WINDOWSTERMINAL_PATH = @"\Local\Microsoft\WindowsApps\wt.exe";
        string TEMP_ASP_FILES = @"\Local\Temp\Temporary ASP.NET Files";
        string OUTLOOK_PATH = @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE";
        string CHROME_PATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        string CONFIG_PATH = @"\src\Pandell.LandRite.Web\Pli.config.devel.jsonc";
        IDictionary<string, string> WEBSITES = new Dictionary<string, string>()
        {
            {"GitHub", "https://github.com/pandell/LandRiteWeb"},
            {"TeamCity", "https://build.pandell.com/"},
            {"LandRiteJira", "https://pandell.atlassian.net/secure/RapidBoard.jspa?rapidView=87"},
            {"Harvest", "https://pandell.harvestapp.com/welcome"},
            { "EmployeePortal", "http://employee/"}
        };

        public MainWindow()
        {
            InitializeComponent();
            UpdateUserPath();
            UpdateConfigPath();
        }

        private void UpdateConfigPath()
        {
            CONFIG_PATH = ProjectPath.Text + CONFIG_PATH;
        }

        private void UpdateUserPath()
        {
            SLACK_PATH = UserPath.Text + SLACK_PATH;
            SPOTIFY_PATH = UserPath.Text + SPOTIFY_PATH;
            WINDOWSTERMINAL_PATH = UserPath.Text + WINDOWSTERMINAL_PATH;
            TEMP_ASP_FILES = UserPath.Text + TEMP_ASP_FILES;
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

        private bool TouchConfigFile()
        {
            try
            {
                var args = "touch " + CONFIG_PATH;
                ProcessCommands.RunCommand(args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        private bool LaunchVsCode()
        {
            return false;
        }

        private void UpdateStatus(string message, bool commandUpdate = false, bool success = false)
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

        private void LaunchApp(Programs program, Func<bool> LaunchFunction)
        {
            var appName = ProgramsTool.GetDescription(program);
            UpdateStatus("Launching " + appName + "...");
            var launchStatus = LaunchFunction();
            if (launchStatus)
                UpdateStatus(appName + " launched.", true, true);
            else
                UpdateStatus(appName + " failed to launch.", true, false);
        }

        private void OnCreatePrClick(object sender, RoutedEventArgs e)
        {
            PrGenerator prGenerator = new PrGenerator();
            prGenerator.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            prGenerator.Show();
        }

        private void OnLaunchClick(object sender, RoutedEventArgs e)
        {
            var slackCheckBox = SlackCheckBox.IsChecked.Value;
            var outlookCheckBox = OutlookCheckBox.IsChecked.Value;
            var chromeCheckBox = ChromeCheckBox.IsChecked.Value;
            var spotifyCheckBox = SpotifyCheckBox.IsChecked.Value;
            var terminalCheckBox = TerminalCheckBox.IsChecked.Value;
            var vsCode = VSCodeCheckBox.IsChecked.Value;
            new Thread(() =>
            {
                UpdateStatus("Launching programs...");

                // Launch slack
                if (slackCheckBox)
                    LaunchApp(Programs.Slack, LaunchSlack);

                // Launch outlook
                if (outlookCheckBox)
                    LaunchApp(Programs.Outlook, LaunchOutlook);

                // Launch chrome website(s)
                if (chromeCheckBox)
                    LaunchApp(Programs.Chrome, LaunchChromeWebsites);

                // Launch spotify
                if (spotifyCheckBox)
                    LaunchApp(Programs.Spotify, LaunchSpotify);

                // Launch terminal
                if (terminalCheckBox)
                    LaunchApp(Programs.WindowsTerminal, LaunchWindowsTerminal);

                // Launch VS Code
                //if (vsCode)
                //    LaunchApp(Programs.VSCode, LaunchVsCode);

                // Delay and wait for programs to launch then resize
                Thread.Sleep(1000);

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

        private void OnIncognitoClick(object sender, RoutedEventArgs e)
        {
            var launchedIncognito = LaunchIncognitoBrowser();
            if (launchedIncognito)
                UpdateStatus("Incognito browser launched.", true, true);
            else
                UpdateStatus("Incognito browser failed to launch.", true, false);
        }

        private void OnTouchConfigFile(object sender, RoutedEventArgs e)
        {
            var touchConfigFile = TouchConfigFile();
            if (touchConfigFile)
            {
                UpdateStatus("Updated config file.", true, true);
            }
            else
            {
                UpdateStatus("Failed to update config file.", true, false);
            }
        }

        private void OnUpdateUserPath(object sender, TextChangedEventArgs e)
        {
            UpdateUserPath();
        }

        private void OnUpdateConfigPath(object sender, TextChangedEventArgs e)
        {
            UpdateConfigPath();
        }
    }
}
 