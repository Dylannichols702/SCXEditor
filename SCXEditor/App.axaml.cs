using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SCXEditor.ViewModels;
using SCXEditor.Views;
using SCXEditor.Services;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace SCXEditor;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };

            var services = new ServiceCollection();

            services.AddSingleton<IFileService>(x => new FileService(desktop.MainWindow));

            Services = services.BuildServiceProvider();
        }

        base.OnFrameworkInitializationCompleted();
    }

    public new static App? Current => Application.Current as App;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider? Services { get; private set; }
}
