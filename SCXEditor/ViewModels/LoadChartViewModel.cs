using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SCXEditor.Models;
using ReactiveUI;
using System.Reactive;
using SCXEditor.Views;
using Avalonia.Controls;

namespace SCXEditor.ViewModels
{
    public class LoadChartViewModel : ViewModelBase
    {
        private string? selectedChart;
        private string? chartDirectory;

        public string[] Charts { 
            get 
            { 
                if (ChartSetManager._ActiveChartSet != null)
                {
                    return Directory.GetFiles(ChartSetManager._ActiveChartSet, "*.xdrv");
                }
                return new string[] { };
            } 
        }
        public string? SelectedChart { get => selectedChart; set => selectedChart = value; }
        public string? ChartDirectory { get => chartDirectory; set => this.RaiseAndSetIfChanged(ref chartDirectory, value); }

        public ReactiveCommand<Unit, Unit> LoadChartCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ChangeChartDirectoryCommand { get; set; }

        public LoadChartViewModel()
        {
            LoadChartCommand = ReactiveCommand.Create(LoadChart);
            ChangeChartDirectoryCommand = ReactiveCommand.Create(ChangeChartDirectory); 
        }

        private async void ChangeChartDirectory()
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

        private void LoadChart()
        {
            XDRV loadedChart = XDRV.DeserializeFromFile(chartDirectory);
            ChartManager._ActiveChart = loadedChart;
        }
    }
}
