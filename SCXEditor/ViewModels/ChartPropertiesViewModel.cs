using ReactiveUI;
using SCXEditor.Models;
using SCXEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Avalonia.Controls;

namespace SCXEditor.ViewModels
{
    public class ChartPropertiesViewModel : ViewModelBase
    {
        private static XDRV? _ActiveChart = ChartManager._ActiveChart;
        private string? _ChartAuthor = _ActiveChart != null ? _ActiveChart.chartMetadata.ChartAuthor : null;
        private Array _Difficulties = Enum.GetValues(typeof(XDRVDifficulty));
        private XDRVDifficulty _SelectedDifficulty = _ActiveChart != null ? _ActiveChart.chartMetadata.ChartDifficulty : XDRVDifficulty.Beginner;
        private int _ChartLevel = _ActiveChart != null ? _ActiveChart.chartMetadata.ChartLevel : 1;
        private float _MusicPreviewStart = _ActiveChart != null ? _ActiveChart.chartMetadata.MusicPreviewStart : 0.00f;
        private float _MusicPreviewLength = _ActiveChart != null ? _ActiveChart.chartMetadata.MusicPreviewLength : 0.00f;
        private int _DisplayBPM = _ActiveChart != null ? _ActiveChart.chartMetadata.ChartDisplayBPM : 150;
        private float _TrueBPM = _ActiveChart != null ? _ActiveChart.chartMetadata.ChartBPM : 150.00f;
        private bool _IsHideRPC = _ActiveChart != null ? _ActiveChart.chartMetadata.RpcHidden : false;
        private int _Volume = _ActiveChart != null ? (int)(_ActiveChart.chartMetadata.MusicVolume * 100) : 100;
        private float _Offset = _ActiveChart != null ? _ActiveChart.chartMetadata.MusicOffset : 0.00f;
        private string? _JacketIllustrator = _ActiveChart != null ? _ActiveChart.chartMetadata.JacketIllustrator : null;
        private string? _SelectedJacket = _ActiveChart != null ? _ActiveChart.chartMetadata.JacketImage : null;

        public string? ChartAuthor { get => _ChartAuthor; set => _ChartAuthor = value; }
        public Array Difficulties { get => _Difficulties;  }
        public XDRVDifficulty SelectedDifficulty { get => _SelectedDifficulty; set => _SelectedDifficulty = value; }
        public int ChartLevel { get => _ChartLevel; set => _ChartLevel = value; }
        public float MusicPreviewStart { get => _MusicPreviewStart; set => _MusicPreviewStart = value; }
        public float MusicPreviewLength { get => _MusicPreviewLength; set => _MusicPreviewLength = value; }
        public int DisplayBPM { get => _DisplayBPM; set => _DisplayBPM = value; }
        public float TrueBPM { get => _TrueBPM; set => _TrueBPM = value; }
        public bool IsHideRPC { get => _IsHideRPC; set => _IsHideRPC = value; }
        public int Volume { get => _Volume; set => _Volume = value; }
        public float Offset { get => _Offset; set => _Offset = value; }
        public string? SelectedJacket { get => _SelectedJacket; set => this.RaiseAndSetIfChanged(ref _SelectedJacket, value); }
        public string? JacketIllustrator { get => _JacketIllustrator; set => _JacketIllustrator = value; }

        public ReactiveCommand<Unit, Unit> SaveChartPropertiesCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ChangeSelectedJacketCommand { get; set; }
        public ChartPropertiesViewModel()
        {
            SaveChartPropertiesCommand = ReactiveCommand.Create(SaveChartProperties);
            ChangeSelectedJacketCommand = ReactiveCommand.Create(ChangeSelectedJacket);
        }

        private async void ChangeSelectedJacket()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AllowMultiple = false;
            FileDialogFilter filter = new FileDialogFilter();
            filter.Extensions.Add("jpg");
            filter.Extensions.Add("png");
            ofd.Filters.Add(filter);
            string[]? input = await ofd.ShowAsync(new MainWindow());
            SelectedJacket = input == null ? null : Path.GetFileName(input[0]);
        }

        private void SaveChartProperties()
        {
            if (_ActiveChart != null)
            {
                // Update the chart file name if the difficulty has changed
                if (_ActiveChart.chartMetadata.ChartDifficulty != SelectedDifficulty)
                {
                    string newChartPath = ChartSetManager._ActiveChartSet + "/" + SelectedDifficulty.ToString().ToUpper() + ".xdrv";
                    File.Copy(_ActiveChart.filePath, newChartPath);
                    File.Delete(_ActiveChart.filePath);
                    _ActiveChart.filePath = newChartPath;
                }

                _ActiveChart.chartMetadata.ChartAuthor = ChartAuthor;
                _ActiveChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                _ActiveChart.chartMetadata.ChartLevel = ChartLevel;
                _ActiveChart.chartMetadata.MusicPreviewStart = MusicPreviewStart;
                _ActiveChart.chartMetadata.MusicPreviewLength = MusicPreviewLength;
                _ActiveChart.chartMetadata.ChartDisplayBPM = DisplayBPM;
                _ActiveChart.chartMetadata.ChartBPM = TrueBPM;
                _ActiveChart.chartMetadata.RpcHidden = IsHideRPC;
                _ActiveChart.chartMetadata.MusicVolume = Volume;
                _ActiveChart.chartMetadata.MusicOffset = Offset;
                _ActiveChart.chartMetadata.JacketImage = SelectedJacket;
                _ActiveChart.chartMetadata.JacketIllustrator = JacketIllustrator;

                _ActiveChart.Serialize();
            }
        }
    }
}
