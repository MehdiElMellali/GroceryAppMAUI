using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using e_Commerce_Grocery.Models;
using e_Commerce_Grocery.Pages;
using e_Commerce_Grocery.Services;
using Grocery.Shared.Dto;
using System.Collections.ObjectModel;

namespace e_Commerce_Grocery.ViewModels
{
    [QueryProperty(nameof(SelectedCategory),nameof(SelectedCategory))]
    public partial class CategoryProductsViewModel : ObservableObject,IDisposable
    {
        private readonly CategoryService _categoryService;
        private readonly ProductsService _productsService;
        private readonly CartViewModel _cartViewModel;

        public CategoryProductsViewModel(CategoryService categoryService
           , ProductsService productsService, CartViewModel cartViewModel)
        {
            _categoryService = categoryService;
            _productsService = productsService;
            _cartViewModel = cartViewModel;

            _cartViewModel.CartCountUpdated += CartViewModel_CartCountUpdated;
            _cartViewModel.CartItemUpdated += CartViewModel_CartItemUpdated;
            _cartViewModel.CartItemRemoved += CartViewModel_CartItemRemoved;
        }

        private void CartViewModel_CartCountUpdated(object sender, int cartCount) => CartCount = cartCount;

        private void CartViewModel_CartItemRemoved(object sender, int productId) => ModifyProductQuantity(productId, 0);

        private void CartViewModel_CartItemUpdated(object sender, CartItem e) => ModifyProductQuantity(e.ProductId, e.Quantity);


        private void ModifyProductQuantity(int productId, int quantity)
        {
            var product = Products.FirstOrDefault(x => x.Id == productId);
            if (product is not null)
            {
                product.CartQuantity = quantity;
            }
        }

        [ObservableProperty,NotifyPropertyChangedFor(nameof(PageTitle))]
        private Category _selectedCategory;

        public string PageTitle => $"{SelectedCategory?.Name ?? "Category"} Products";

        [ObservableProperty]
        private IEnumerable<Category> _categories= Enumerable.Empty<Category>();

        //ObservableProperty create get and set automaticlty like fody
        [ObservableProperty]
        private IEnumerable<ProductDto> _products = Enumerable.Empty<ProductDto>();

        //public ObservableCollection<Category> Categories { get; set; } = new();

        //public ObservableCollection<ProductDto> Products { get; set; } = new();

        [ObservableProperty]
        private bool _isBusy = true;

        [ObservableProperty]
        private int _cartCount;

        public async Task InitializeAsync()
        {
            IsBusy = true;
            try
            {
                //Categories.Clear();
                //foreach (var category in await _categoryService.GetSubbrSiblingCategories(SelectedCategory.Id))
                //{
                //    Categories.Add(category);
                //}
                //Products.Clear();
                //foreach (var product in await _productsService.GetCategoryProductsAsync(SelectedCategory.Id))
                //{
                //    Products.Add(product);
                //}
                var categoryTask = _categoryService.GetSubbrSiblingCategories(SelectedCategory.Id);
                var productTask = await _productsService.GetCategoryProductsAsync(SelectedCategory.Id);

                Categories = await categoryTask;
                Products =  productTask;

            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void AddToCart(int productId) => UpdateCart(productId, 1);

        [RelayCommand]
        private void RemoveFromCart(int productId) => UpdateCart(productId, -1);

        private void UpdateCart(int productId, int count)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product is not null)
            {
                product.CartQuantity += count;

                if (count == -1)
                {
                    // We are removing from cart
                    _cartViewModel.RemoveFromCartCommand.Execute(product.Id);
                }
                else
                {
                    //Adding to cart
                    _cartViewModel.AddToCartCommand.Execute(product);
                }
                CartCount = _cartViewModel.Count;
            }
        }


        public void Dispose()
        {
            _cartViewModel.CartCountUpdated -= CartViewModel_CartCountUpdated;
            _cartViewModel.CartItemUpdated -= CartViewModel_CartItemUpdated;
            _cartViewModel.CartItemRemoved -= CartViewModel_CartItemRemoved;
        }
    }
}
