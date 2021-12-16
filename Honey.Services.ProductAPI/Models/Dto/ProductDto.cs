﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Honey.Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
