namespace SCXEditor.ViewModels;
using ReactiveUI;
using SCXEditor.Models;
using SCXEditor.Views;
using System;
using System.Reactive;

public class MainViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> ShowNewChartSetWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowLoadChartSetWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowNewChartWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowLoadChartWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowChartSetPropertiesWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowChartPropertiesWindowCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ShowModsMenuWindowCommand { get; set; }

    public MainViewModel()
    {
        ShowNewChartWindowCommand = ReactiveCommand.Create(ShowNewChartWindow);
        ShowLoadChartWindowCommand = ReactiveCommand.Create(ShowLoadChartWindow);
        ShowChartPropertiesWindowCommand = ReactiveCommand.Create(ShowChartPropertiesWindow);
        ShowModsMenuWindowCommand = ReactiveCommand.Create(ShowModsMenuWindow);
    }

    private void ShowModsMenuWindow()
    {
        ModsMenu modsMenuWin = new ModsMenu();
        modsMenuWin.Show();
    }

    private void ShowChartPropertiesWindow()
    {
        if (ChartManager._ActiveChart != null) {
            ChartProperties chartPropertiesWin = new ChartProperties();
            chartPropertiesWin.Show();
        }
    }

    private void ShowLoadChartWindow()
    {
        LoadChart loadChartWin = new LoadChart();
        loadChartWin.Show();
    }

    private void ShowNewChartWindow()
    {
        NewChart newChartWin = new NewChart();
        newChartWin.Show();
    }
}
