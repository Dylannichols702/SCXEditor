using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;

namespace SCXEditor.Views;

public partial class LoadChartSet : Window
{
    public LoadChartSet()
    {
        InitializeComponent();
        this.DataContext = new LoadChartSetViewModel();
    }
}