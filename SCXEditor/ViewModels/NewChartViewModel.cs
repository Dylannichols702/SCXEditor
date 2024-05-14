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
using Microsoft.Extensions.DependencyInjection;
using SCXEditor.Services;
using Avalonia.Platform.Storage;

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
            var fileService = App.Current?.Services?.GetService<IFileService>();
            if (fileService is null) throw new NullReferenceException("Missing File Service instance.");

            var folder = await fileService.OpenFolderAsync();
            if (folder is null) return;

            ChartSetDirectory = folder.Path.LocalPath;
        }

        [RelayCommand]
        private async Task ChangeAudioFileName()
        {
            var fileService = App.Current?.Services?.GetService<IFileService>();
            if (fileService is null) throw new NullReferenceException("Missing File Service instance.");

            var file = await fileService.OpenFileAsync(new[] { FileService.AudioFiles });
            if (file is null) return;

            AudioFileName = file.Path.LocalPath;
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
