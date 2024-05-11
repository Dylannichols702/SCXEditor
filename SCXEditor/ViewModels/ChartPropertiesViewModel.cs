using ReactiveUI;
using SCXEditor.Models;
using SCXEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ViewModels
{
    public class ChartPropertiesViewModel : ViewModelBase
    {
        private static XDRV? activeChart = ChartManager._ActiveChart;
        private string? _ChartAuthor = activeChart != null ? activeChart.chartMetadata.ChartAuthor : null;
        private Array _Difficulties = Enum.GetValues(typeof(XDRVDifficulty));
        private XDRVDifficulty _SelectedDifficulty = activeChart != null ? activeChart.chartMetadata.ChartDifficulty : XDRVDifficulty.Beginner;
        private int _ChartLevel = activeChart != null ? activeChart.chartMetadata.ChartLevel : 1;

        private float _MusicPreviewStart = activeChart != null ? activeChart.chartMetadata.MusicPreviewStart : 0.00f;
        private float _MusicPreviewLength = activeChart != null ? activeChart.chartMetadata.MusicPreviewLength : 0.00f;

        private int _DisplayBPM = activeChart != null ? activeChart.chartMetadata.ChartDisplayBPM : 150;
        private float _TrueBPM = activeChart != null ? activeChart.chartMetadata.ChartBPM : 150.00f;

        private bool _IsHideRPC = activeChart != null ? activeChart.chartMetadata.RpcHidden : false;

        public string? ChartAuthor { get => _ChartAuthor; set => _ChartAuthor = value; }
        public Array Difficulties { get => _Difficulties;  }
        public XDRVDifficulty SelectedDifficulty { get => _SelectedDifficulty; set => _SelectedDifficulty = value; }
        public int ChartLevel { get => _ChartLevel; set => _ChartLevel = value; }

        public float MusicPreviewStart { get => _MusicPreviewStart; set => _MusicPreviewStart = value; }
        public float MusicPreviewLength { get => _MusicPreviewLength; set => _MusicPreviewLength = value; }

        public int DisplayBPM { get => _DisplayBPM; set => _DisplayBPM = value; }
        public float TrueBPM { get => _TrueBPM; set => _TrueBPM = value; }

        public bool IsHideRPC { get => _IsHideRPC; set => _IsHideRPC = value; }

        public ReactiveCommand<Unit, Unit> SaveChartPropertiesCommand { get; set; }

        public ChartPropertiesViewModel()
        {
            SaveChartPropertiesCommand = ReactiveCommand.Create(SaveChartProperties);
        }

        private void SaveChartProperties()
        {
            XDRV? activeChart = ChartManager._ActiveChart;

            if (activeChart != null)
            {
                activeChart.chartMetadata.ChartAuthor = ChartAuthor;
                activeChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                activeChart.chartMetadata.ChartLevel = ChartLevel;
                activeChart.chartMetadata.MusicPreviewStart = MusicPreviewStart;
                activeChart.chartMetadata.MusicPreviewLength = MusicPreviewLength;
                activeChart.chartMetadata.ChartDisplayBPM = DisplayBPM;
                activeChart.chartMetadata.ChartBPM = TrueBPM;
                activeChart.chartMetadata.RpcHidden = IsHideRPC;
                activeChart.Serialize();
            }
        }
    }
}
