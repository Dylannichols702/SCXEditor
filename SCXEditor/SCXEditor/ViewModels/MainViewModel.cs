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
    internal class MainViewModel : BaseViewModel
    {
        // TODO: Refactor this logic to only require one ICommand
        public ICommand ShowNewChartSetWindowCommand { get; set; }
        public ICommand ShowNewChartWindowCommand { get; set; }
        public ICommand ShowLoadChartWindowCommand { get; set; }
        public ICommand ShowLoadChartSetWindowCommand { get; set; }
        public ICommand ShowChartSetPropertiesWindowCommand { get; set; }
        public ICommand ShowChartPropertiesWindowCommand { get; set; }
        public ICommand ShowModsMenuWindowCommand { get; set; }

        public MainViewModel() { 
            ShowNewChartSetWindowCommand = new RelayCommand(ShowChartSetWindow, CanShowChartSetWindow);
            ShowNewChartWindowCommand = new RelayCommand(ShowNewChartWindow, CanShowNewChartWindow);
            ShowLoadChartWindowCommand = new RelayCommand(ShowLoadChartWindow, CanShowLoadChartWindow);
            ShowLoadChartSetWindowCommand = new RelayCommand(ShowLoadChartSetWindow, CanShowLoadChartSetWindow);
            ShowChartSetPropertiesWindowCommand = new RelayCommand(ShowChartSetPropertiesWindow, CanShowChartSetPropertiesWindow);
            ShowChartPropertiesWindowCommand = new RelayCommand(ShowChartPropertiesWindow, CanShowChartPropertiesWindow);
            ShowModsMenuWindowCommand = new RelayCommand(ShowModsMenuWindow, CanShowModsMenuWindow);
        }

        private bool CanShowModsMenuWindow(object obj)
        {
            return true;
        }

        private void ShowModsMenuWindow(object obj)
        {
            ModsMenu modsMenuWin = new ModsMenu();
            modsMenuWin.Show();
        }

        private bool CanShowChartPropertiesWindow(object obj)
        {
            return true;
        }

        private void ShowChartPropertiesWindow(object obj)
        {
            ChartProperties chartPropertiesWin = new ChartProperties();
            chartPropertiesWin.Show();
        }

        private bool CanShowChartSetPropertiesWindow(object obj)
        {
            return true;
        }

        private void ShowChartSetPropertiesWindow(object obj)
        {
            ChartSetProperties chartSetPropertiesWin = new ChartSetProperties();
            chartSetPropertiesWin.Show();
        }

        private bool CanShowLoadChartSetWindow(object obj)
        {
            return true;
        }

        private void ShowLoadChartSetWindow(object obj)
        {
            LoadChartSet loadChartSetWin = new LoadChartSet();
            loadChartSetWin.Show();
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
