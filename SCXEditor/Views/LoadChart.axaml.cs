using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;

namespace SCXEditor.Views;

public partial class LoadChart : Window
{
    public LoadChart()
    {
        InitializeComponent();
        this.DataContext = new LoadChartViewModel();
    }
}