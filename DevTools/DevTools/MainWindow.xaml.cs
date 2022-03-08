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
        const string SLACK_PATH = @"C:\Users\joshv\AppData\Local\slack\slack.exe";
        const string SPOTIFY_PATH = @"C:\Users\joshv\AppData\Roaming\Spotify\Spotify.exe";
        const string WINDOWSTERMINAL_PATH = @"C:\Users\joshv\AppData\Local\Microsoft\WindowsApps\wt.exe";
        const string TEMP_ASP_FILES = @"C:\Users\joshv\AppData\Local\Temp\Temporary ASP.NET Files";
        const string OUTLOOK_PATH = @"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE";
        const string CHROME_PATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        const string CONFIG_PATH = @"\src\";
        IDictionary<string, string> WEBSITES = new Dictionary<string, string>()
        {
            {"GitHub", "https://github.com/pandell/LandRiteWeb"},
            {"TeamCity", "https://build.pandell.com/"},
            {"LandRiteJira", "https://pandell.atlassian.net/secure/RapidBoard.jspa?rapidView=87"},
            {"Harvest", "https://pandell.harvestapp.com/welcome"},
            { "EmployeePortal", "http://employee/"}
        };

        int HORIZONTAL_SCREEN_WIDTH = (int)SystemParameters.PrimaryScreenWidth;
        int HORIZONTAL_SCREEN_HEIGHT = (int)SystemParameters.PrimaryScreenHeight;

        public MainWindow()
        {
            InitializeComponent();
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

        private bool LaunchVsCode()
        {
            return false;
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
        private bool CheckTimeSheetSubmission()
        {
            var buttons = MessageBoxButton.YesNoCancel;
            var result = MessageBox.Show("Make sure to submit your time sheet for approval! Did you submit already?", "Time Sheet Submission Reminder", buttons);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        private bool ResizeWindowRightHalfTopVertical(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowRightHalfTopVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
        }

        private bool ResizeWindowCenterMaximizedHorizontal(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowCenterMaximizedHorizontal(targetWindow);
        }

        private bool ResizeWindowRightHalfBottomVertical(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowRightHalfBottomVertical(targetWindow, HORIZONTAL_SCREEN_WIDTH, HORIZONTAL_SCREEN_HEIGHT);
        }

        private bool ResizeWindowLeftMaximizedVertical(Programs targetWindow)
        {
            return WindowCommands.ResizeWindowLeftMaximizedVertical(targetWindow);
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

        private void ResizeApp(Programs program, Func<Programs, bool> ResizeFunction)
        {
            var appName = ProgramsTool.GetDescription(program);
            UpdateStatus("Resizing " + appName + "...");
            var resizeStatus = ResizeFunction(program);
            if (resizeStatus)
                UpdateStatus(appName + " resized.", true, true);
            else
                UpdateStatus(appName + " failed to resize.", true, false);
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
            var vpnCheckBox = VpnCheckBox.IsChecked.Value;
            new Thread(() =>
            {
                UpdateStatus("Launching programs...");

                // Connect to vpn
                if (vpnCheckBox)
                    LaunchApp(Programs.VPN, ConnectToVpn);

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

                // Delay and wait for programs to launch then resize
                Thread.Sleep(1000);

                // Resizing Slack window
                if (slackCheckBox)
                    ResizeApp(Programs.Slack, ResizeWindowRightHalfTopVertical);

                // Resizing Outlook window
                if (outlookCheckBox)
                    ResizeApp(Programs.Outlook, ResizeWindowRightHalfBottomVertical);

                // Resizing Chrome window
                if (chromeCheckBox)
                    ResizeApp(Programs.Chrome, ResizeWindowCenterMaximizedHorizontal);

                // Resizing Spotify window
                if (spotifyCheckBox)
                    ResizeApp(Programs.Spotify, ResizeWindowLeftMaximizedVertical);
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
            return;
            var slackCheckBox = SlackCheckBox.IsChecked.Value;
            var outlookCheckBox = OutlookCheckBox.IsChecked.Value;
            var chromeCheckBox = ChromeCheckBox.IsChecked.Value;
            var spotifyCheckBox = SpotifyCheckBox.IsChecked.Value;
            new Thread(() =>
            {
                // Resizing Slack window
                if (slackCheckBox)
                    ResizeApp(Programs.Slack, ResizeWindowRightHalfTopVertical);
                
                // Resizing Outlook window
                if (outlookCheckBox)
                    ResizeApp(Programs.Outlook, ResizeWindowRightHalfBottomVertical);

                // Resizing Chrome window
                if (chromeCheckBox)
                    ResizeApp(Programs.Chrome, ResizeWindowCenterMaximizedHorizontal);

                // Resizing Spotify window
                if (spotifyCheckBox)
                    ResizeApp(Programs.Spotify, ResizeWindowLeftMaximizedVertical);
            }).Start();
        }

        private DayOfWeek GetDayOfWeekFromDate(DateTime currentDate, int targetDate) {
            return new DateTime(currentDate.Year, currentDate.Month, targetDate).DayOfWeek;
        }

        private void CheckPayDay() {
            var currentDate = DateTime.Now;
            var numDays = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            
            var biWeeklySundayPayDay = GetDayOfWeekFromDate(currentDate, (numDays / 2) - 2);
            var biWeeklySaturdayPayDay = GetDayOfWeekFromDate(currentDate, (numDays / 2) - 1);
            var biWeeklyWeekDayPayDay = GetDayOfWeekFromDate(currentDate, numDays / 2);
            
            if ((biWeeklySundayPayDay == DayOfWeek.Friday) || 
                (biWeeklySaturdayPayDay == DayOfWeek.Friday) || 
                (biWeeklyWeekDayPayDay != DayOfWeek.Saturday || biWeeklyWeekDayPayDay != DayOfWeek.Sunday)) {
                MessageBox.Show("It's halfway through the month today so it's pay day today!");
                return;
            }
            
            var endOfMonthSundayPayDay = GetDayOfWeekFromDate(currentDate, (numDays / 2) - 2);
            var endOfMonthSaturdayPayDay = GetDayOfWeekFromDate(currentDate, (numDays / 2) - 1);
            var endOfMonthWeekDayPayDay = GetDayOfWeekFromDate(currentDate, numDays / 2);

            if ((endOfMonthSundayPayDay == DayOfWeek.Friday) || 
                (endOfMonthSaturdayPayDay == DayOfWeek.Friday) || 
                (endOfMonthWeekDayPayDay != DayOfWeek.Saturday || endOfMonthWeekDayPayDay != DayOfWeek.Sunday)) {
                MessageBox.Show("It's end of the month today so it's pay day today!");
                return;
            }
        }

        private void OnHomeTimeClick(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                if (!CheckTimeSheetSubmission())
                    return;
            }

            CheckPayDay();
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
            
        }
    }
}
 