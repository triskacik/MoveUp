using MoveUp.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoveUp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation navigation;
        private readonly IMvvmLocatorService mvvmLocatorService;

        public NavigationService(INavigation navigation, IMvvmLocatorService mvvmLocatorService)
        {
            this.navigation = navigation;
            this.mvvmLocatorService = mvvmLocatorService;
        }

        public async Task PopAsync(bool animated = false)
        {
            await navigation.PopAsync(animated);
        }

        public async Task PopToRootAsync(bool animated = false)
        {
            await navigation.PopToRootAsync(animated);
        }

        public async Task PushAsync<TViewModel>(TViewModel viewModel = default, bool animated = false, bool clearHistory = false)
        {
            try
            {
                var view = mvvmLocatorService.ResolveView(viewModel);
                await navigation.PushAsync(view, animated);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
