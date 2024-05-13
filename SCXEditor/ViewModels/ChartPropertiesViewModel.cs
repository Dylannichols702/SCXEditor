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
        public ChartPropertiesViewModel() { }

        [ObservableProperty] private static XDRV? activeChart;
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
            if (ActiveChart != null)
            {
                // Update the chart file name if the difficulty has changed
                if (ActiveChart.chartMetadata.ChartDifficulty != SelectedDifficulty)
                {
                    string newChartPath = ChartSetManager._ActiveChartSet + "/" + SelectedDifficulty.ToString().ToUpper() + ".xdrv";
                    File.Copy(ActiveChart.filePath, newChartPath);
                    File.Delete(ActiveChart.filePath);
                    ActiveChart.filePath = newChartPath;
                }

                ActiveChart.chartMetadata.ChartAuthor = ChartAuthor;
                ActiveChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                ActiveChart.chartMetadata.ChartLevel = ChartLevel;
                ActiveChart.chartMetadata.MusicPreviewStart = MusicPreviewStart;
                ActiveChart.chartMetadata.MusicPreviewLength = MusicPreviewLength;
                ActiveChart.chartMetadata.ChartDisplayBPM = DisplayBPM;
                ActiveChart.chartMetadata.ChartBPM = TrueBPM;
                ActiveChart.chartMetadata.RpcHidden = IsHideRPC;
                ActiveChart.chartMetadata.MusicVolume = Volume;
                ActiveChart.chartMetadata.MusicOffset = Offset;
                ActiveChart.chartMetadata.JacketImage = SelectedJacket;
                ActiveChart.chartMetadata.JacketIllustrator = JacketIllustrator;

                ActiveChart.Serialize();
            }
        }
    }
}
