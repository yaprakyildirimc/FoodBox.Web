namespace FoodBox.Core.Models
{
    public class StoreProduct : EntitiyBase
    {
        public Store Store { get; set; }

        public Product Product { get; set; }

        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}
