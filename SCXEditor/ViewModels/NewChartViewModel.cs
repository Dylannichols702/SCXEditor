using ReactiveUI;
using SCXEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.ViewModels
{
    public class NewChartViewModel : ViewModelBase
    {
        private Array diffs = Enum.GetValues(typeof(XDRVDifficulty));
        private string songTitle;
        private string songArtist;
        private XDRVDifficulty selectedDiff;
        private int difficultyLevel;
        private string chartArtist;

        public Array Difficulties { get => diffs; }
        public int DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
        public XDRVDifficulty SelectedDifficulty { get => selectedDiff; set => selectedDiff = value; }
        public string SongTitle { get => songTitle; set => songTitle = value; }
        public string SongArtist { get => songArtist; set => songArtist = value; }
        public string ChartArtist { get => chartArtist; set => chartArtist = value; }

        public ReactiveCommand<Unit, Unit> CreateChartCommand { get; set; }

        public NewChartViewModel()
        {
            CreateChartCommand = ReactiveCommand.Create(CreateChart);
        }

        private void CreateChart()
        {
            if (ChartSetManager._ActiveChartSet != null)
            {
                XDRV newChart = new XDRV(ChartSetManager._ActiveChartSet + "/" + SelectedDifficulty.ToString().ToUpper() + ".xdrv");
                newChart.chartMetadata.MusicTitle = SongTitle;
                newChart.chartMetadata.MusicArtist = SongArtist;
                newChart.chartMetadata.ChartLevel = DifficultyLevel;
                newChart.chartMetadata.ChartDifficulty = SelectedDifficulty;
                newChart.chartMetadata.ChartAuthor = ChartArtist;
                newChart.Serialize();
            }
        }
    }
}
