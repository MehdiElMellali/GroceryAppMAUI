using e_Commerce_Grocery.Pages;

namespace e_Commerce_Grocery;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
	}
}
