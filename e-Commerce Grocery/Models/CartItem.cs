using CommunityToolkit.Mvvm.ComponentModel;

namespace e_Commerce_Grocery.Models
{
    public partial class CartItem : ObservableObject
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]
        public int _quantity;

        public decimal Amount => Price * Quantity;

    }

}
