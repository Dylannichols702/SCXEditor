using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SCXEditor.Models;
using ReactiveUI;
using System.Reactive;

namespace SCXEditor.ViewModels
{
    public class LoadChartViewModel : ViewModelBase
    {
        private string[] charts = Directory.GetFiles(ChartSetManager._ActiveChartSet, "*.xdrv");
        private string selectedChart;

        public string[] Charts { get => charts; set => charts = value; }
        public string SelectedChart { get => selectedChart; set => selectedChart = value; }

        public ReactiveCommand<Unit, Unit> LoadChartCommand { get; set; }
        public LoadChartViewModel()
        {
            LoadChartCommand = ReactiveCommand.Create(LoadChart);
        }

        private void LoadChart()
        {
            XDRV loadedChart = XDRV.DeserializeFromFile(selectedChart);
            Console.WriteLine(loadedChart);
        }
    }
}
