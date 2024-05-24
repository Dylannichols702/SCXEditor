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
using System.Data.Common;

public partial class MainViewModel : ViewModelBase

{
    public MainViewModel()
    {
        // Wake necessary singletons (yowza)
        EditorManager editorManager = EditorManager.Instance;
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

    [RelayCommand]
    private void IncrementQuantization()
    {
        InputManager.Instance.DKeyPressed();
    }

    [RelayCommand]
    private void DecrementQuantization()
    {
        InputManager.Instance.AKeyPressed();
    }

    [RelayCommand]
    private void TraverseRowForward()
    {
        InputManager.Instance.WKeyPressed();
    }

    [RelayCommand]
    private void TraverseRowBackward()
    {
        InputManager.Instance.SKeyPressed();
    }

    [RelayCommand]
    private void PlaceTapNote(int column)
    {
        InputManager.Instance.TapNoteKeyPressed(column);
    }

    [RelayCommand]
    private void PlaceHoldNote(int column)
    {
        InputManager.Instance.HoldNoteKeyPressed(column);
    }

    [RelayCommand]
    private void SaveChart()
    {
        InputManager.Instance.SaveHotkeyPressed();
    }
}
