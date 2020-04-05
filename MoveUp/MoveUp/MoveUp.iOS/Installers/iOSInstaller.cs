using Microsoft.Extensions.DependencyInjection;
using MoveUp.Factories.Interfaces;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using MoveUp.Views;
using Xamarin.Forms;

namespace MoveUp.iOS.Installers
{
    public class iOSInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
                .FromAssemblyOf<iOSInstaller>()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
