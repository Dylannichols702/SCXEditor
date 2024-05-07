using ReactiveUI;
using SCXEditor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ViewModels
{
    public class ChartSetPropertiesViewModel : ViewModelBase
    {
        private int _Volume = 100;
        private float _Offset = 0.00f;
        private string? _Illustrator;
        private string? _SelectedJacket;

        public string? ChartSetDirectory 
        { 
            get 
            {
                string? activeChartSet = ChartSetManager._ActiveChartSet;
                if (activeChartSet != null)
                {
                    return ChartSetManager._ActiveChartSet;
                }
                return "No Chart Set Selected";
            } 
        }
        public int Volume { get => _Volume; set => _Volume = value; }
        public float Offset { get => _Offset; set => _Offset = value; }
        public string[] Jackets 
        { 
            get
            {
                if (ChartSetManager._ActiveChartSet != null)
                {
                    // TODO: Abstract this into a method to get image files of many types.
                    return Directory.GetFiles(ChartSetManager._ActiveChartSet, "*.jpg");
                }
                return new string[] { };
            } 
        }
        public string? SelectedJacket { get => _SelectedJacket; set => _SelectedJacket = value; }
        public string? Illustrator { get => _Illustrator; set => _Illustrator = value; }

        public ReactiveCommand<Unit, Unit> SaveChartSetPropsCommand { get; set; }
        public ChartSetPropertiesViewModel()
        {
            SaveChartSetPropsCommand = ReactiveCommand.Create(SaveChartSetProps);
        }

        private void SaveChartSetProps()
        {
            // TODO: This is yucky, make a separate method to sync props changes.
            string? activeChartSet = ChartSetManager._ActiveChartSet;
            if (activeChartSet != null)
            {
                string[] charts = Directory.GetFiles(activeChartSet, "*.xdrv");
                foreach (string chart in charts)
                {
                    XDRV chartObject = XDRV.DeserializeFromFile(chart);
                    chartObject.chartMetadata.MusicVolume = (float)Volume/100;
                    chartObject.chartMetadata.MusicOffset = Offset;
                    chartObject.chartMetadata.JacketImage = Path.GetFileName(SelectedJacket);
                    chartObject.chartMetadata.JacketIllustrator = Illustrator;
                    chartObject.Serialize();
                }
            }
        }
    }
}
