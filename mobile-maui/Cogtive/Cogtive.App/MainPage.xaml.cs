using Cogtive.App.ViewModels;

namespace Cogtive.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageVM viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
