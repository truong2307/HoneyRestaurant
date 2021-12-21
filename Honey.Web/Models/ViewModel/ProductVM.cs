using Honey.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Honey.Web.Models.ViewModel
{
    public class ProductVM
    {
        public ProductDto Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
