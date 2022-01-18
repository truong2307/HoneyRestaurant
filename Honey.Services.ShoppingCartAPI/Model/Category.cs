using System.ComponentModel.DataAnnotations;

namespace Honey.Services.ShoppingCartAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
