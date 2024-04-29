﻿using ReactiveUI;
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

namespace SCXEditor.ViewModels
{
    public class NewChartSetViewModel : ViewModelBase
    {
        private string chartSetDirectory = "";
        private string songTitle = "";
        private string songArtist = "";
        public string ChartSetDirectory 
        { 
            get => chartSetDirectory;
            set => this.RaiseAndSetIfChanged(ref chartSetDirectory, value); 
        }
        public string SongTitle 
        {
            get => songTitle;
            set => this.RaiseAndSetIfChanged(ref songTitle, value);
        }
        public string SongArtist
        {
            get => songArtist;
            set => this.RaiseAndSetIfChanged(ref songArtist, value);
        }

        public ReactiveCommand<Unit, Unit> ChangeChartSetDirectoryCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CreateChartSetCommand { get; set; }

        public NewChartSetViewModel()
        {
            ChangeChartSetDirectoryCommand = ReactiveCommand.Create(ChangeChartSetDirectory);
            CreateChartSetCommand = ReactiveCommand.Create(CreateChartSet);
        }

        private void CreateChartSet()
        {
            DirectoryInfo chartSet = Directory.CreateDirectory(ChartSetDirectory + "/" + songTitle + " - " + songArtist);
            ChartSetManager._ActiveChartSet = chartSet.FullName;
        }

        private async void ChangeChartSetDirectory()
        {
            // TODO: This method is jank and deprecated, fix it later.
            OpenFolderDialog ofd = new OpenFolderDialog();
            ChartSetDirectory = await ofd.ShowAsync(new MainWindow());
        }
    }
}
