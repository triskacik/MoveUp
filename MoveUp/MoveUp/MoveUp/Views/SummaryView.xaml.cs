using MoveUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoveUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryView : ContentPage
    {
        public SummaryView()
        {
            BindingContext = new SummaryViewModel();
            InitializeComponent();
        }

        private void Button_Pressed(object sender, EventArgs e)
        {

        }
    }
}