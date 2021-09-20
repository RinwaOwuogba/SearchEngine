using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SearchEngineClient.ViewModels;
using SearchEngineClient.Views;
using SearchEngine;


namespace SearchEngineClient
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Engine engine = new Engine("../repository/");
                Querier querier = new Querier(engine);
                Watcher watch = new Watcher(engine);

                // run file watcher in the background
                Watcher.IndexInBackGround(watch);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(querier, engine.GetMetaInfo),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}