using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SCXEditor.Views;

public partial class NewChartSet : Window
{
    public NewChartSet()
    {
        InitializeComponent();
        this.DataContext = new SCXEditor.ViewModels.NewChartSetViewModel();
    }
}