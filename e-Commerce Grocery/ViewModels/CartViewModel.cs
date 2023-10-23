using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using e_Commerce_Grocery.Models;
using Grocery.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce_Grocery.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public event EventHandler<int> CartCountUpdated;
        public event EventHandler<CartItem> CartItemUpdated;
        public event EventHandler<int> CartItemRemoved;

        public ObservableCollection<CartItem> CartItems { get; set; } = new();

        [ObservableProperty]
        private int _count; //The number of products we have in the cart (Not the quantities of those products)

        [ObservableProperty]
        private decimal _totalAmount;

        private void RecalculateTotalAmount() =>
            TotalAmount = CartItems.Sum(c => c.Amount);

        [RelayCommand]
        private void IncreaseCartItemQuantity(Guid cartItemId)
        {
            var item = CartItems.FirstOrDefault(c => c.Id == cartItemId);
            if (item is not null)
            {
                item.Quantity++;
                RecalculateTotalAmount();
                CartItemUpdated.Invoke(this, item);
            }
        }

        [RelayCommand]
        private void AddToCart(ProductDto product)
        {
            var item = CartItems.FirstOrDefault(c => c.ProductId == product.Id);
            if (item is not null)
            {
                item.Quantity++;
                CartItemUpdated.Invoke(this, item);
            }
            else
            {
                item = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductName = product.Name,
                    ProductId = product.Id,
                    Quantity = 1,
                    Price = product.Price,
                };
                CartItems.Add(item);
                Count = CartItems.Count;
                CartCountUpdated.Invoke(this, Count);
            }
            RecalculateTotalAmount();
            CartItemUpdated.Invoke(this, item);
        }

        [RelayCommand]
        private void RemoveFromCart(int productId)
        {
            var item = CartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item is not null)
            {
                if (item.Quantity == 1)
                {
                    CartItems.Remove(item);
                    Count = CartItems.Count;
                    CartItemRemoved.Invoke(this, productId);
                    CartCountUpdated.Invoke(this, Count);
                }
                else
                {
                    item.Quantity--;
                    CartItemUpdated.Invoke(this, item);
                }
            }
            RecalculateTotalAmount();
        }

        private void ClearCart()
        {
            CartItems.Clear();
            Count = 0;
            RecalculateTotalAmount();
        }
    }
}