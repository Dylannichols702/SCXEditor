using SCXEditor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using SCXEditor.Models;
using System.Windows.Data;
using System.ComponentModel;

namespace SCXEditor.ViewModels
{
    class NewChartSetViewModel : BaseViewModel
    {
        public string ChartSetDirectory { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }

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
            OnPropertyChanged("ChartSetDirectory");
        }

        private bool CanCreateChartSet(object obj)
        {
            // TODO: Refactor this portion to take advantage of the RelayCommand predicate
            return true;
        }

        private void CreateChartSet(object obj)
        {
            if (ChartSetDirectory != null)
            {
                ChartSetDirectory = ChartSetDirectory + "\\" + SongTitle + " - " + SongArtist;
                ((NewChartSet)obj).Close();
                Directory.CreateDirectory(ChartSetDirectory);
                ChartSetManager.GetActiveChartSet().ChartSetDirectory = ChartSetDirectory;
                System.Windows.MessageBox.Show("Created a new Chart Set at " + ChartSetManager.GetActiveChartSet().ChartSetDirectory);
            } else
            {
                System.Windows.MessageBox.Show("Please provide a chart set directory before continuing.");
            }
        }
    }
}
