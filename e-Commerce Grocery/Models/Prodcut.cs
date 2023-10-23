namespace e_Commerce_Grocery.Models
{
    public class Prodcut
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public short CategoryId { get; set; }

        public string CategoryName { get; set; }

    }

}
