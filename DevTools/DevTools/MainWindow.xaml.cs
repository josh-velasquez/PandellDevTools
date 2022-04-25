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
        //string SLACK_PATH = @"C:\Users\joshv\AppData\Local\slack\slack.exe";
        //string SPOTIFY_PATH = @"C:\Users\joshv\AppData\Roaming\Spotify\Spotify.exe";
        //string WINDOWSTERMINAL_PATH = @"C:\Users\joshv\AppData\Local\Microsoft\WindowsApps\wt.exe";
        //string TEMP_ASP_FILES = @"C:\Users\joshv\AppData\Local\Temp\Temporary ASP.NET Files";
        //string CONFIG_PATH = @"D:\Repository\LandRiteWeb\src\Pandell.LandRite.Web\Pli.config.devel.jsonc";
        //string VS_CODE_PATH = @"C:\Users\joshv\AppData\Local\Programs\Microsoft VS Code\Code.exe"

        //string OUTLOOK_PATH = @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE";
        //string CHROME_PATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        const string SLACK_PATH = @"\Local\slack\slack.exe";
        const string SPOTIFY_PATH = @"\Roaming\Spotify\Spotify.exe";
        const string WINDOWS_TERMINAL_PATH = @"\Local\Microsoft\WindowsApps\wt.exe";
        const string TEMP_ASP_FILES = @"\Local\Temp\Temporary ASP.NET Files";
        const string CONFIG_PATH = @"\src\Pandell.LandRite.Web\Pli.config.devel.jsonc";
        const string VS_CODE_PATH = @"\Local\Programs\Microsoft VS Code\Code.exe";

        const string OUTLOOK_PATH = @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE";
        const string CHROME_PATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        
        string USER_SLACK_PATH = "";
        string USER_SPOTIFY_PATH = "";
        string USER_WINDOWS_TERMINAL_PATH = "";
        string USER_TEMP_ASP_FILES = "";
        string USER_CONFIG_PATH = "";
        string USER_VS_CODE_PATH = "";
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

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData";
            UserPath.Text = appDataPath;
        }

        private bool LaunchSlack()
        {
            try
            {
                return ProcessCommands.RunCommand(USER_SLACK_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchOutlook()
        {
            try
            {
                return ProcessCommands.RunCommand(OUTLOOK_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchChromeWebsites()
        {
            try
            {
                var results = true;
                foreach (var website in WEBSITES)
                {
                    var result = ProcessCommands.RunCommand(CHROME_PATH, website.Value);
                    results = results && result;
                }
                return results;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchSpotify()
        {
            try
            {
                return ProcessCommands.RunCommand(USER_SPOTIFY_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchWindowsTerminal()
        {
            try
            {
                return ProcessCommands.RunCommand(USER_WINDOWS_TERMINAL_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchIncognitoBrowser()
        {
            try
            {
                var args = "google.com -incognito";
                return ProcessCommands.RunCommand(CHROME_PATH, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool TouchConfigFile()
        {
            try
            {
                var args = "touch " + USER_CONFIG_PATH;
                return ProcessCommands.RunCommand(args);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
            return false;
        }

        private bool LaunchVsCode()
        {
            try
            {
                //var args = "start " + USER_VS_CODE_PATH;
                return ProcessCommands.RunCommand(USER_VS_CODE_PATH);
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
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

                //Launch VS Code
                if (vsCode)
                    LaunchApp(Programs.VSCode, LaunchVsCode);

                // Delay and wait for programs to launch then resize
                Thread.Sleep(1000);

            }).Start();
        }

        private void OnDelTempAspFilesClick(object sender, RoutedEventArgs e)
        {
            UpdateStatus("Deleting Temporary ASP.NET Files...", false);
            try
            {
                ProcessCommands.DeleteDirectory(USER_TEMP_ASP_FILES);
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

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            StatusListBox.Items.Clear();
        }

        private void OnUserPathChange(object sender, TextChangedEventArgs e)
        {
            var userPath = UserPath.Text;
            USER_SLACK_PATH = userPath + SLACK_PATH;
            USER_SPOTIFY_PATH = userPath + SPOTIFY_PATH;
            USER_WINDOWS_TERMINAL_PATH = userPath + WINDOWS_TERMINAL_PATH;
            USER_TEMP_ASP_FILES = userPath + TEMP_ASP_FILES;
            USER_VS_CODE_PATH = userPath + VS_CODE_PATH;
        }

        private void OnProjectPathChange(object sender, TextChangedEventArgs e)
        {
            var projectPath = ProjectPath.Text;
            USER_CONFIG_PATH = projectPath + CONFIG_PATH;
        }
    }
}
 