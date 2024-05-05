using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;

namespace SCXEditor.Views;

public partial class ChartSetProperties : Window
{
    public ChartSetProperties()
    {
        InitializeComponent();
        this.DataContext = new ChartSetPropertiesViewModel();
    }
}