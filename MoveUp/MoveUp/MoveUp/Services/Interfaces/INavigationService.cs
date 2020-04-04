using System.Threading.Tasks;

namespace MoveUp.Services.Interfaces
{
    public interface INavigationService : ISingletonService
    {
        Task PushAsync<TViewModel>(TViewModel viewModel = default, bool animated = default, bool clearHistory = default);
        Task PopAsync(bool animated = default);
        Task PopToRootAsync(bool animated = default);
    }
}