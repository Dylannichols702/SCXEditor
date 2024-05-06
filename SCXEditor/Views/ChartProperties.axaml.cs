using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;
using System;

namespace SCXEditor.Views;

public partial class ChartProperties : Window
{
    public ChartProperties()
    {
        InitializeComponent();
        this.DataContext = new ChartPropertiesViewModel();
    }
}