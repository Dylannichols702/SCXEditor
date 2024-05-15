namespace SCXEditor.ViewModels;
using ReactiveUI;
using SCXEditor.Models;
using SCXEditor.Views;
using System;
using System.Reactive;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ChartRenderer;

public partial class MainViewModel : ViewModelBase

{
    [ObservableProperty] Game game = new ChartRenderer();
    public MainViewModel()
    {

    }

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

    // TODO: Create an event system that isn't as tightly coupled
    [RelayCommand]
    private void IncrementQuantization()
    {
        EditorManager.IncrementQuantization();
    }

    [RelayCommand]
    private void DecrementQuantization()
    {
        EditorManager.DecrementQuantization();
    }

    [RelayCommand]
    private void TraverseRowForward()
    {
        EditorManager.TraverseRowForward();
    }

    [RelayCommand]
    private void TraverseRowBackward()
    {
        EditorManager.TraverseRowBackward();
    }

    [RelayCommand]
    private void PlaceNote(int column)
    {
        EditorManager.PlaceNote(column);
    }

    [RelayCommand]
    private void SaveChart()
    {
        ChartManager._ActiveChart?.Serialize();
    }
}
