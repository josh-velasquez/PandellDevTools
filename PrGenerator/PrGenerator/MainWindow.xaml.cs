using System.IO;
using System.Text;
using System.Windows;

namespace DevTools
{
    /// <summary>
    /// Interaction logic for PrGenerator.xaml
    /// </summary>
    public partial class PrGenerator : Window
    {
        string[] JiraTicketTypes = { "LRW", "GIS", "PRJ" };
        public PrGenerator()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            foreach (var val in JiraTicketTypes)
            {
                JiraTicketType.Items.Add(val);
            }
            JiraTicketType.SelectedIndex = 0;
        }

        const string ROOT_URL = "https://pandell.atlassian.net/browse/";

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            var jiraNumber = JiraNumberTextBox.Text;
            var jiraHeader = JiraHeaderTextBox.Text;
            var jiraDescription = JiraDescriptionTextBox.Text;
            if (string.IsNullOrEmpty(jiraNumber))
            {
                MessageBox.Show("Please enter a JIRA story number.");
                return;
            }
            else if (string.IsNullOrEmpty(jiraDescription))
            {
                MessageBox.Show("Please enter a JIRA description");
                return;
            }
            else if (string.IsNullOrEmpty(jiraHeader))
            {
                MessageBox.Show("Please enter a JIRA header");
                return;
            }
            var title = generateTitle(jiraNumber, jiraHeader);
            var pullRequest = buildPullRequestBody(jiraNumber, jiraDescription);
            TitleTextBox.Text = title;
            BodyTextBox.Text = pullRequest;
            MessageBox.Show("Don't forget to assign the Pull Request to yourself!");
        }

        private string buildPullRequestBody(string jiraNumber, string jiraDescription)
        {
            var pullRequest = new StringBuilder();
            var header = generateHeader(jiraNumber);
            pullRequest.AppendLine(header);
            pullRequest.AppendLine();
            var description = generateDescription(jiraDescription);
            pullRequest.AppendLine(description);
            pullRequest.AppendLine("---");
            pullRequest.AppendLine("<YOUR SUMMARY HERE>");
            return pullRequest.ToString();
        }

        private string generateTitle(string jiraNumber, string jiraHeader)
        {
            var title = JiraTicketType.SelectedItem + "-" + jiraNumber + " " + jiraHeader;
            return title;
        }

        private string generateHeader(string jiraNumber)
        {
            var header = "[JIRA " + JiraTicketType.SelectedItem + "-" + jiraNumber + "](" + ROOT_URL + JiraTicketType.SelectedItem + "-" + jiraNumber + ")";
            return header;
        }

        private static string generateDescription(string description)
        {
            var newDescription = new StringBuilder();
            using (StringReader reader = new StringReader(description))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    newDescription.AppendLine("> " + line);
                }
            }
            return newDescription.ToString();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            JiraNumberTextBox.Text = "";
            JiraHeaderTextBox.Text = "";
            JiraDescriptionTextBox.Text = "";
            TitleTextBox.Text = "";
            BodyTextBox.Text = "";
        }

        private void OnCopyTitleClick(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text;
            Clipboard.SetText(title);
        }

        private void OnCopyBodyClick(object sender, RoutedEventArgs e)
        {
            var body = BodyTextBox.Text;
            Clipboard.SetText(body);
        }
    }
}
