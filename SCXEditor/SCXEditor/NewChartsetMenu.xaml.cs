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

namespace SCXEditor
{
    /// <summary>
    /// Interaction logic for NewChartsetMenu.xaml
    /// </summary>
    public partial class NewChartsetMenu : Window
    {
        public NewChartsetMenu()
        {
            InitializeComponent();
        }

        private void newChartsetNextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            NewChartMenu newChartMenu = new NewChartMenu();
            newChartMenu.Show();
        }
    }
}
