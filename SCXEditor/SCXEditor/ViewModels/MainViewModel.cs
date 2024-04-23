using Record_Book_MVVM.Commands;
using SCXEditor.Models;
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

        public MainViewModel() { 
            ShowNewChartSetWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            NewChartSet newChartSetWin = new NewChartSet();
            newChartSetWin.Show();
        }
    }
}
