using ValidationRulesTest.Views;

namespace ValidationRulesTest;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new Examples());
	}
}
