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
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Platform;
using SkiaSharp;
using Avalonia.Skia;
using Avalonia.Media;

public partial class MainViewModel : ViewModelBase

{
    public RenderTargetBitmap RenderTarget { get; }
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
    private void PlaceNote(int column)
    {
        InputManager.Instance.NoteKeyPressed(column);
    }

    [RelayCommand]
    private void SaveChart()
    {
        InputManager.Instance.SaveHotkeyPressed();
    }
}
