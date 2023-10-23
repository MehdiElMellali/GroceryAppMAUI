using e_Commerce_Grocery.ViewModels;

namespace e_Commerce_Grocery.Pages;

public partial class CartPage : ContentPage
{
	public readonly CartViewModel _cartViewModel;
    public CartPage(CartViewModel cartViewModel)
	{
		InitializeComponent();
		_cartViewModel = cartViewModel;
		BindingContext = _cartViewModel;
	}
}