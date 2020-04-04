using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoveUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBase : ContentPage
    {
        public ViewBase()
        {
            InitializeComponent();
        }
    }
}
