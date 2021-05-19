using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;

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
            var repoSourceDirectory = "D:\\Repository\\LandRiteWeb";
            SrcDirTextBox.Text = repoSourceDirectory;
        }

        private void OnCreatePrClick(object sender, RoutedEventArgs e)
        {
            PrGenerator prGenerator = new PrGenerator();
            prGenerator.Show();
        }

        private void OnClobberClick(object sender, RoutedEventArgs e)
        {
            StatusListBox.Items.Add("Clobbering files...");
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
            StatusListBox.Items.Add("Launching dev tool...");
        }


        private void ConnectToVpn()
        {
            try
            {
                // wait for 2 seconds
                Thread.Sleep(2000);

                // Move and to tray icon
                Tools.SetPosition(2351, (1440 - 16));
                Tools.SendClick();
                Thread.Sleep(500);

                // Move and click to connect icon
                Tools.SetPosition(2401, (1440 - 77));
                Tools.SendClick();
                Thread.Sleep(10000);

                // close tray
                Tools.SetPosition(2351, (1440 - 16));
                Tools.SendClick();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error: " + e);
            }
        }

        private void LaunchSlack()
        {
            Process.Start(SLACK_PATH);
        }

        private void LaunchOutlook()
        {
            Process.Start(OUTLOOK_PATH);
            //PowershellTerminal.SendCommand(OUTLOOK_PATH);
        }

        private void LaunchChromeWebsites(string url)
        {
            Process.Start(CHROME_PATH, url);
        }

        private void LaunchSpotify()
        {
            Process.Start(SPOTIFY_PATH);
        }

        private void LaunchWindowsTerminal()
        {
            Process.Start(WINDOWSTERMINAL_PATH);
        }

        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        private void ResizeAppRightTopVertical(string targetApp) {
            Process process = Process.GetProcessesByName(targetApp)[0];
            IntPtr handle = process.MainWindowHandle;
            Tools.WindowPositioning windowPositioning = new Tools.WindowPositioning();
            if (Tools.GetWindowRect(handle, ref windowPositioning))
            {
                Tools.MoveWindow(handle, HORIZONTAL_SCREEN_WIDTH, -300, HORIZONTAL_SCREEN_HEIGHT, (HORIZONTAL_SCREEN_WIDTH/2)-15, true);
            }
        }
        // -15 to account for task bar
        // task bar height = 30 pixels so 30/2 = 15 and remove 15 pixels to each height (if half)
        private void ResizeAppRightBottomVertical(string targetApp)
        {
            Process process = Process.GetProcessesByName(targetApp)[0];
            IntPtr handle = process.MainWindowHandle;
            Tools.WindowPositioning windowPositioning = new Tools.WindowPositioning();
            if (Tools.GetWindowRect(handle, ref windowPositioning))
            {
                Tools.MoveWindow(handle, HORIZONTAL_SCREEN_WIDTH, 965, HORIZONTAL_SCREEN_HEIGHT, (HORIZONTAL_SCREEN_WIDTH / 2) - 15, true);
            }
        }

        private void ResizeAppLeftFullVertical(string targetApp)
        {
            Process process = Process.GetProcessesByName(targetApp)[0];
            IntPtr handle = process.MainWindowHandle;
            Tools.WindowPositioning windowPositioning = new Tools.WindowPositioning();
            if (Tools.GetWindowRect(handle, ref windowPositioning))
            {
                // Move to the left
                Tools.MoveWindow(handle, -2000, 0, windowPositioning.right, windowPositioning.top, true);
                // Maximize
                Tools.ShowWindow(handle, Tools.SW_SHOWMAXIMIZED);
            }
        }

        private void OnLaunchClick(object sender, RoutedEventArgs e)
        {
            //StatusListBox.Items.Add("Launching programs...");
            //// Connect to vpn
            //StatusListBox.Items.Add("Connecting to VPN...");
            ////ConnectToVpn();
            //StatusListBox.Items.Add("Connected to VPN.");
            //// launch slack
            //StatusListBox.Items.Add("Launching Slack...");
            //LaunchSlack();
            //// launch outlook
            //StatusListBox.Items.Add("Launching Outlook...");
            //LaunchOutlook();
            //// launch chrome
            //foreach (var website in WEBSITES)
            //{
            //    LaunchChromeWebsites(website.Value);
            //}
            //// launch spotify
            //StatusListBox.Items.Add("Launching Spotify...");
            //LaunchSpotify();
            //// launch terminal
            //LaunchWindowsTerminal();
            //ResizeAppRightTopVertical("slack");
            //ResizeAppRightBottomVertical("outlook");
            ResizeAppLeftFullVertical("chrome");
        }

        private void OnDelTempAspFilesClick(object sender, RoutedEventArgs e)
        {
            var tempFiles = "C:\\Users\\joshv\\AppData\\Local\\Temp\\Temporary ASP.NET Files";
            StatusListBox.Items.Add("Deleting Temporary ASP.NET Files...");
        }
    }
}
