namespace SCXEditor.ViewModels;
using ReactiveUI;
using SCXEditor.Views;
using System;
using System.Reactive;

public class MainViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> ShowNewChartSetWindowCommand { get; set; }

    public MainViewModel()
    {
        ShowNewChartSetWindowCommand = ReactiveCommand.Create(ShowNewChartSetWindow);
    }

    private void ShowNewChartSetWindow()
    {
        // TODO: Add logic for creating a new chart set directory
    }
}
