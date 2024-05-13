namespace SCXEditor.ViewModels;
using ReactiveUI;
using SCXEditor.Models;
using SCXEditor.Views;
using System;
using System.Reactive;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

public partial class MainViewModel : ViewModelBase
{
    [RelayCommand]
    private void ShowModsMenuWindow()
    {
        ModsMenu modsMenuWin = new ModsMenu();
        modsMenuWin.Show();
    }

    [RelayCommand]
    private void ShowChartPropertiesWindow()
    {
        if (ChartManager._ActiveChart != null) {
            ChartProperties chartPropertiesWin = new ChartProperties();
            chartPropertiesWin.Show();
        }
    }

    [RelayCommand]
    private void ShowLoadChartWindow()
    {
        LoadChart loadChartWin = new LoadChart();
        loadChartWin.Show();
    }

    [RelayCommand]
    private void ShowNewChartWindow()
    {
        NewChart newChartWin = new NewChart();
        newChartWin.Show();
    }
}
