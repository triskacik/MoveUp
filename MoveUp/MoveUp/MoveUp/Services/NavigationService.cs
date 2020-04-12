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
            await navigation.PopAsync(true);
        }

        public async Task PopToRootAsync(bool animated = false)
        {
            await navigation.PopToRootAsync(true);
        }

        public async Task PushAsync<TViewModel>(TViewModel viewModel = default, bool animated = true, bool clearHistory = false)
        {
            try
            {
                var view = mvvmLocatorService.ResolveView(viewModel);
                await navigation.PushAsync(view, true);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
