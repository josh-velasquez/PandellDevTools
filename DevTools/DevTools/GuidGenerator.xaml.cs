using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevTools
{
    /// <summary>
    /// Interaction logic for GuidGenerator.xaml
    /// </summary>
    public partial class GuidGenerator : Window
    {
        public GuidGenerator()
        {
            InitializeComponent();
        }

        private void OnGenerateGuidClick(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.NewGuid();
            GuidTextBox.Text = "\"" +guid.ToString() + "\"";
        }

        private void OnCopyClick(object sender, RoutedEventArgs e)
        {
            var guid = GuidTextBox.Text;
            Clipboard.SetText(guid);
        }
    }
}
