using System.ComponentModel.DataAnnotations;

namespace Honey.Services.ShoppingCartAPI.Model.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
