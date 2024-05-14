using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SCXEditor.Models;
using SCXEditor.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SCXEditor.ViewModels
{
    public partial class LoadChartViewModel : ViewModelBase
    {
        [ObservableProperty] private string? chartDirectory;

        public LoadChartViewModel()
        {

        }

        [RelayCommand]
        private async Task ChangeChartDirectory()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AllowMultiple = false;
            FileDialogFilter filter = new FileDialogFilter();
            filter.Extensions.Add("xdrv");
            ofd.Filters.Add(filter);
            string[]? input = await ofd.ShowAsync(new MainWindow());
            ChartDirectory = input == null ? null : input[0];
        }

        [RelayCommand]
        private void LoadChart()
        {
            XDRV loadedChart = XDRV.DeserializeFromFile(ChartDirectory);
            ChartManager._ActiveChart = loadedChart;
        }
    }
}
