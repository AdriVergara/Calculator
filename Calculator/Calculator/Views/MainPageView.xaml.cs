using Calculator.ViewModels;
using Xamarin.Forms;

namespace Calculator
{
	public partial class MainPageView : ContentPage
	{
		public MainPageView()
		{
            //MainPage = new NavigationPage();

            BindingContext = new MainPageViewModel();
            //BindingContext = new FirstPageVM(Navigation);

            InitializeComponent();
		}
	}
}
