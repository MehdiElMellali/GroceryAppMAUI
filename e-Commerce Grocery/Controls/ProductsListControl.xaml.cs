using CommunityToolkit.Mvvm.Input;
using Grocery.Shared.Dto;

namespace e_Commerce_Grocery.Controls;

public class ProductCartItemChangeEventArgs : EventArgs
{
    public int ProductId { get; set; }
    public int Count { get; set; }

    public ProductCartItemChangeEventArgs(int productId, int count)
    {
        ProductId = productId;
        Count = count;
    }
}

public partial class ProductsListControl : ContentView
{
    public static readonly BindableProperty ProductsProperty =
       BindableProperty.Create(nameof(Products), typeof(IEnumerable<ProductDto>), typeof(ProductsListControl), Enumerable.Empty<ProductDto>());

    public ProductsListControl()
	{
		InitializeComponent();
	}

    public IEnumerable<ProductDto> Products
    {
        get => (IEnumerable<ProductDto>)GetValue(ProductsProperty);
        set => SetValue(ProductsProperty, value);
    }

    public event EventHandler<ProductCartItemChangeEventArgs> AddRemoveCartClicked;

    [RelayCommand]
    private void AddToCart(int productId) =>
        AddRemoveCartClicked?.Invoke(this, new ProductCartItemChangeEventArgs(productId, 1));

    [RelayCommand]
    private void RemoveFromCart(int productId) =>
        AddRemoveCartClicked?.Invoke(this, new ProductCartItemChangeEventArgs(productId, -1));
}