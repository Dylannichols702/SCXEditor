using SCXEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;
using SCXEditor.Models;

namespace SCXEditor.ViewModels
{
    public class LoadChartSetViewModel : ViewModelBase
    {
        private string? chartSetDirectory;
        private string? songTitle;
        private string? songArtist;
        public string? ChartSetDirectory 
        { 
            get => chartSetDirectory; 
            set => this.RaiseAndSetIfChanged(ref chartSetDirectory, value); 
        }

        public string? SongTitle
        {
            get => songTitle;
            set => songTitle = value;
        }

        public string? SongArtist
        {
            get => songArtist;
            set => songArtist = value;
        }

        public ReactiveCommand<Unit, Unit> ChangeChartSetDirectoryCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LoadChartSetCommand { get; set; }

        public LoadChartSetViewModel()
        {
            ChangeChartSetDirectoryCommand = ReactiveCommand.Create(ChangeChartSetDirectory);
            LoadChartSetCommand = ReactiveCommand.Create(LoadChartSet);
        }

        private async void ChangeChartSetDirectory()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFolderDialog ofd = new OpenFolderDialog();
            ChartSetDirectory = await ofd.ShowAsync(new MainWindow());
            ChartManager._ActiveChart = null;
        }

        private void LoadChartSet()
        {
            if (ChartSetDirectory != "") 
            {
                ChartSetManager._ActiveChartSet = ChartSetDirectory;
            }
        }
    }
}
