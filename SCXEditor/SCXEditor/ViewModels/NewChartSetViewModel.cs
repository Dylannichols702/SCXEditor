using SCXEditor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace SCXEditor.ViewModels
{
    class NewChartSetViewModel
    {
        public string ChartSetDirectory { get; set; }

        public ICommand CreateChartSetCommand { get; set; }
        public ICommand ShowFolderBrowserCommand { get; set; }

        public NewChartSetViewModel()
        {
            CreateChartSetCommand = new RelayCommand(CreateChartSet, CanCreateChartSet);
            ShowFolderBrowserCommand = new RelayCommand(ShowFolderBrowser, CanShowFolderBrowser);
        }

        private bool CanShowFolderBrowser(object obj)
        {
            return true;
        }

        private void ShowFolderBrowser(object obj)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            ChartSetDirectory = fbd.SelectedPath;
        }

        private bool CanCreateChartSet(object obj)
        {
            return true;
        }

        private void CreateChartSet(object obj)
        {
            // TODO: Add logic for creating a new chart set
            System.Windows.MessageBox.Show(ChartSetDirectory);
        }
    }
}
