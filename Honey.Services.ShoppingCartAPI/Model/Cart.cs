using System.Collections.Generic;

namespace Honey.Services.ShoppingCartAPI.Model
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
