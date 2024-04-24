using SCXEditor.Commands;
using SCXEditor.Models;
using SCXEditor.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SCXEditor.ViewModels
{
    internal class MainViewModel
    {

        public ICommand ShowNewChartSetWindowCommand { get; set; }
        public ICommand ShowNewChartWindowCommand { get; set; }

        public MainViewModel() { 
            ShowNewChartSetWindowCommand = new RelayCommand(ShowChartSetWindow, CanShowChartSetWindow);
            ShowNewChartWindowCommand = new RelayCommand(ShowChartWindow, CanShowChartWindow);
        }

        private bool CanShowChartWindow(object obj)
        {
            return true;
        }

        private void ShowChartWindow(object obj)
        {
            NewChart newChartWin = new NewChart();
            newChartWin.Show();
        }

        private bool CanShowChartSetWindow(object obj)
        {
            return true;
        }

        private void ShowChartSetWindow(object obj)
        {
            NewChartSet newChartSetWin = new NewChartSet();
            newChartSetWin.Show();
        }
    }
}
