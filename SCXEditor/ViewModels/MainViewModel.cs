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
        ShowNewChartSetWindowCommand = ReactiveCommand.Create(ShowNewChartSetWindow);
        ShowLoadChartSetWindowCommand = ReactiveCommand.Create(ShowLoadChartSetWindow);
        ShowNewChartWindowCommand = ReactiveCommand.Create(ShowNewChartWindow);
        ShowLoadChartWindowCommand = ReactiveCommand.Create(ShowLoadChartWindow);
        ShowChartSetPropertiesWindowCommand = ReactiveCommand.Create(ShowChartSetPropertiesWindow);
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
        ChartProperties chartPropertiesWin = new ChartProperties();
        chartPropertiesWin.Show();
    }

    private void ShowChartSetPropertiesWindow()
    {
        ChartSetProperties chartSetPropertiesWin = new ChartSetProperties();
        chartSetPropertiesWin.Show();
    }

    private void ShowLoadChartWindow()
    {
        if (ChartSetManager._ActiveChartSet != null) 
        {
            LoadChart loadChartWin = new LoadChart();
            loadChartWin.Show();
        } 
    }

    private void ShowNewChartWindow()
    {
        if (ChartSetManager._ActiveChartSet != null)
        {
            NewChart newChartWin = new NewChart();
            newChartWin.Show();
        }
    }

    private void ShowLoadChartSetWindow()
    {
        LoadChartSet loadChartSetWin = new LoadChartSet();
        loadChartSetWin.Show();
    }

    private void ShowNewChartSetWindow()
    {
        NewChartSet newChartSetWin = new NewChartSet();
        newChartSetWin.Show();
    }
}
