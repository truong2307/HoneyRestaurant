using System.ComponentModel.DataAnnotations;

namespace Honey.Web.Models.Dto
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,100)]
        public int Count { get; set; }
    }
}
