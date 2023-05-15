using AutoMapper;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using RentalService.Client.ViewModels;
using RentalService.Client.Views;
using RentalService.Server.Dto;
using Splat;

namespace RentalService.Client;

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
            Locator.CurrentMutable.RegisterConstant(new ApiWrapper());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApiClient.ClientGetDto, ClientViewModel>();
                cfg.CreateMap<ClientViewModel, ApiClient.ClientGetDto>();
            });
            Locator.CurrentMutable.RegisterConstant(config.CreateMapper(), typeof(IMapper));
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}