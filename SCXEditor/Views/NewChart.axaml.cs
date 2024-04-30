using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;

namespace SCXEditor.Views;

public partial class NewChart : Window
{
    public NewChart()
    {
        InitializeComponent();
        this.DataContext = new NewChartViewModel();
    }
}