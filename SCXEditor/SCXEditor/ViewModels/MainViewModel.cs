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
        public ICommand ShowLoadChartWindowCommand { get; set; }

        public MainViewModel() { 
            ShowNewChartSetWindowCommand = new RelayCommand(ShowChartSetWindow, CanShowChartSetWindow);
            ShowNewChartWindowCommand = new RelayCommand(ShowNewChartWindow, CanShowNewChartWindow);
            ShowLoadChartWindowCommand = new RelayCommand(ShowLoadChartWindow, CanShowLoadChartWindow);
        }

        private bool CanShowLoadChartWindow(object obj)
        {
            return true;
        }

        private void ShowLoadChartWindow(object obj)
        {
            LoadChart loadChartWin = new LoadChart();
            loadChartWin.Show();
        }

        private bool CanShowNewChartWindow(object obj)
        {
            return true;
        }

        private void ShowNewChartWindow(object obj)
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
