using System;
using Akavache;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Serilog;
using Sextant;
using Shiny;
using SocialQ.Forms.Queue;
using SocialQ.Forms.Stores;
using SocialQ.Mocks.Queue;
using SocialQ.Queue;
using Splat.Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace SocialQ.Forms
{
    public class SocialQStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            RxApp.DefaultExceptionHandler = new SocialQExceptionHandler();
            BlobCache.ApplicationName = $"{nameof(SocialQ)}";

            services.UseNotifications(); // set true
            services
                .AddSerilog(() => new LoggerConfiguration())
                .AddSextant()
                .AddAkavache()
                .RegisterForNavigation<MainPage, MainViewModel>()
                .RegisterForNavigation<Queues, QueuesViewModel>()
                .RegisterForNavigation<StoreSearch, StoreSearchViewModel>()
                .AddApiContracts(true)
                .AddApiClients()
                .AddDataServices()
                .AddSingleton(SignalRParameters.Client)
                .UseMicrosoftDependencyResolver();
        }

        public static Page NavigateToStart<T>()
            where T : IViewModel
        {
           ShinyHost.Container.GetService<IParameterViewStackService>().PushPage<T>().Subscribe();
            return (Page) ShinyHost.Container.GetService<IView>();
        }
    }
}