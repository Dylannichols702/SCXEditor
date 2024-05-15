using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SCXEditor.Models;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SCXEditor.Services;
using System.Threading;

namespace SCXEditor.ViewModels
{
    public partial class LoadChartViewModel : ViewModelBase
    {
        [ObservableProperty] private string? chartDirectory;

        public LoadChartViewModel()
        {

        }

        [RelayCommand]
        private async Task ChangeChartDirectory(CancellationToken token)
        {
            var fileService = App.Current?.Services?.GetService<IFileService>();
            if (fileService is null) throw new NullReferenceException("Missing File Service instance.");

            var file = await fileService.OpenFileAsync(new[] { FileService.XDRV });
            if (file is null) return;

            ChartDirectory = file.Path.LocalPath;
        }

        [RelayCommand]
        private void LoadChart()
        {
            XDRV loadedChart = XDRV.DeserializeFromFile(ChartDirectory);
            ChartManager._ActiveChart = loadedChart;
        }
    }
}
