using SCXEditor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SCXEditor.ViewModels
{
    class NewChartSetViewModel
    {
        public ICommand CreateChartSetCommand { get; set; }

        public NewChartSetViewModel()
        {
            CreateChartSetCommand = new RelayCommand(CreateChartSet, CanCreateChartSet);
        }

        private bool CanCreateChartSet(object obj)
        {
            return true;
        }

        private void CreateChartSet(object obj)
        {
            // TODO: Add logic for creating a new chart set
        }
    }
}
