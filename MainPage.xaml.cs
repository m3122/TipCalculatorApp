namespace TipCalculatorApp;
using TipCalculatorApp.ViewModels;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new TipCalculatorViewModel();
	}

}

