using e_Commerce_Grocery.ViewModels;

namespace e_Commerce_Grocery.Pages;

public partial class CategoryProductsPage : ContentPage
{
	public readonly CategoryProductsViewModel _viewModel;
    public CategoryProductsPage(CategoryProductsViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.InitializeAsync();
    }

    private void ProductsListControl_AddRemoveCartClicked(object sender, Controls.ProductCartItemChangeEventArgs e)
    {
        if (e.Count > 0)
        {
            _viewModel.AddToCartCommand.Execute(e.ProductId);
        }
        else
        {
            _viewModel.RemoveFromCartCommand.Execute(e.ProductId);
        }
    }
}