using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia;
using SCXEditor.Views;
using System.IO;
using SCXEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SCXEditor.ViewModels
{
    public partial class NewChartViewModel : ViewModelBase
    {
        public NewChartViewModel() { }

        [ObservableProperty] private Array difficulties = Enum.GetValues(typeof(XDRVDifficulty));
        [ObservableProperty] private string? songTitle;
        [ObservableProperty] private string? songArtist;
        [ObservableProperty] private XDRVDifficulty selectedDifficulty;
        [ObservableProperty] private int difficultyLevel;
        [ObservableProperty] private string? chartArtist;
        [ObservableProperty] private string? chartSetDirectory;
        [ObservableProperty] private string? audioFileName;

        [RelayCommand]
        private async Task ChangeChartDirectory()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFolderDialog ofd = new OpenFolderDialog();
            ChartSetDirectory = await ofd.ShowAsync(new MainWindow());
        }

        [RelayCommand]
        private async Task ChangeAudioFileName()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AllowMultiple = false;
            FileDialogFilter filter = new FileDialogFilter();
            filter.Extensions.Add("mp3"); 
            filter.Extensions.Add("ogg");
            filter.Extensions.Add("wav");
            ofd.Filters.Add(filter);
            string[]? input = await ofd.ShowAsync(new MainWindow());
            AudioFileName = input == null ? null : Path.GetFileName(input[0]);
        }

        [RelayCommand]
        private void CreateChart()
        {
            if (ChartSetDirectory != null)
            {
                XDRV newChart = new XDRV(ChartSetDirectory + "/" + SelectedDifficulty.ToString().ToUpper() + ".xdrv");
                newChart.chartMetadata.MusicTitle = SongTitle;
                newChart.chartMetadata.MusicArtist = SongArtist;
                newChart.chartMetadata.ChartLevel = DifficultyLevel;
                newChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                newChart.chartMetadata.ChartAuthor = ChartArtist;
                newChart.chartMetadata.MusicAudio = AudioFileName;
                ChartManager._ActiveChart = newChart;
                newChart.Serialize();
            }
        }
    }
}
