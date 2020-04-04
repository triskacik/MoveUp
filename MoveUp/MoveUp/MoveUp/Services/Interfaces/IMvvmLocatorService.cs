using MoveUp.Services.Interfaces;
using Xamarin.Forms;

namespace MoveUp.Services.Interfaces
{
    public interface IMvvmLocatorService : ISingletonService
    {
        Page ResolveView<TViewModel>(TViewModel viewModel = default);
    }
}