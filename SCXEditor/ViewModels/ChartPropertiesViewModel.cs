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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SCXEditor.ViewModels
{
    public partial class ChartPropertiesViewModel : ViewModelBase
    {
        const int VOLUME_CONVERSION_FACTOR = 100;

        private static XDRV? activeChart;
        [ObservableProperty] private string? chartAuthor;
        [ObservableProperty] private Array? difficulties;
        [ObservableProperty] private XDRVDifficulty selectedDifficulty;
        [ObservableProperty] private int chartLevel;
        [ObservableProperty] private float musicPreviewStart;
        [ObservableProperty] private float musicPreviewLength;
        [ObservableProperty] private int displayBPM;
        [ObservableProperty] private float trueBPM;
        [ObservableProperty] private bool isHideRPC;
        [ObservableProperty] private int volume;
        [ObservableProperty] private float offset;
        [ObservableProperty] private string? jacketIllustrator;
        [ObservableProperty] private string? selectedJacket;

        public ChartPropertiesViewModel()
        {
            UpdateProps();
        }

        [RelayCommand]
        private async Task ChangeSelectedJacket()
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

        [RelayCommand]
        private void SaveChartProperties()
        {
            if (activeChart != null)
            {
                // Update the chart file name if the difficulty has changed
                if (activeChart.chartMetadata.ChartDifficulty != SelectedDifficulty)
                {
                    string newChartPath = ChartSetManager._ActiveChartSet + "/" + SelectedDifficulty.ToString().ToUpper() + ".xdrv";
                    File.Copy(activeChart.filePath, newChartPath);
                    File.Delete(activeChart.filePath);
                    activeChart.filePath = newChartPath;
                }

                activeChart.chartMetadata.ChartAuthor = ChartAuthor;
                activeChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                activeChart.chartMetadata.ChartLevel = ChartLevel;
                activeChart.chartMetadata.MusicPreviewStart = MusicPreviewStart;
                activeChart.chartMetadata.MusicPreviewLength = MusicPreviewLength;
                activeChart.chartMetadata.ChartDisplayBPM = DisplayBPM;
                activeChart.chartMetadata.ChartBPM = TrueBPM;
                activeChart.chartMetadata.RpcHidden = IsHideRPC;
                activeChart.chartMetadata.MusicVolume = ((float)Volume)/VOLUME_CONVERSION_FACTOR;
                activeChart.chartMetadata.MusicOffset = Offset;
                activeChart.chartMetadata.JacketImage = SelectedJacket;
                activeChart.chartMetadata.JacketIllustrator = JacketIllustrator;

                activeChart.Serialize();
            }
        }

        private void UpdateProps()
        {
            activeChart = ChartManager._ActiveChart;
            ChartAuthor = activeChart != null ? activeChart.chartMetadata.ChartAuthor : null;
            Difficulties = Enum.GetValues(typeof(XDRVDifficulty));
            SelectedDifficulty = activeChart != null ? activeChart.chartMetadata.ChartDifficulty : XDRVDifficulty.Beginner;
            ChartLevel = activeChart != null ? activeChart.chartMetadata.ChartLevel : 1;
            MusicPreviewStart = activeChart != null ? activeChart.chartMetadata.MusicPreviewStart : 0.00f;
            MusicPreviewLength = activeChart != null ? activeChart.chartMetadata.MusicPreviewLength : 0.00f;
            DisplayBPM = activeChart != null ? activeChart.chartMetadata.ChartDisplayBPM : 150;
            TrueBPM = activeChart != null ? activeChart.chartMetadata.ChartBPM : 150.00f;
            IsHideRPC = activeChart != null ? activeChart.chartMetadata.RpcHidden : false;
            Volume = activeChart != null ? (int)(activeChart.chartMetadata.MusicVolume * VOLUME_CONVERSION_FACTOR) : 100;
            Offset = activeChart != null ? activeChart.chartMetadata.MusicOffset : 0.00f;
            JacketIllustrator = activeChart != null ? activeChart.chartMetadata.JacketIllustrator : null;
            SelectedJacket = activeChart != null ? activeChart.chartMetadata.JacketImage : null;
        }
    }
}
