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
using static System.Net.Mime.MediaTypeNames;

namespace SCXEditor.ViewModels
{
    public class NewChartViewModel : ViewModelBase
    {
        private Array diffs = Enum.GetValues(typeof(XDRVDifficulty));
        private string? songTitle;
        private string? songArtist;
        private XDRVDifficulty selectedDiff;
        private int difficultyLevel;
        private string? chartArtist;
        private string? chartSetDirectory;
        private string? audioFileName;

        public Array Difficulties { get => diffs; }
        public int DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
        public XDRVDifficulty SelectedDifficulty { get => selectedDiff; set => selectedDiff = value; }
        public string? SongTitle { get => songTitle; set => songTitle = value; }
        public string? SongArtist { get => songArtist; set => songArtist = value; }
        public string? ChartArtist { get => chartArtist; set => chartArtist = value; }
        public string? ChartSetDirectory
        {
            get => chartSetDirectory;
            set => this.RaiseAndSetIfChanged(ref chartSetDirectory, value);
        }
        public string? AudioFileName
        {
            get => audioFileName;
            set => this.RaiseAndSetIfChanged(ref audioFileName, value);
        }


        public ReactiveCommand<Unit, Unit> CreateChartCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ChangeChartDirectoryCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ChangeAudioFileNameCommand { get; set; }

        public NewChartViewModel()
        {
            CreateChartCommand = ReactiveCommand.Create(CreateChart);
            ChangeChartDirectoryCommand = ReactiveCommand.Create(ChangeChartDirectory);
            ChangeAudioFileNameCommand = ReactiveCommand.Create(ChangeAudioFileName);
        }

        private async void ChangeChartDirectory()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFolderDialog ofd = new OpenFolderDialog();
            ChartSetDirectory = await ofd.ShowAsync(new MainWindow());
        }

        private async void ChangeAudioFileName()
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
