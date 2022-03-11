namespace Honey.Web
{
    public static class SD
    {
        public const string SessionShoppingCart = "ShoppingCart";
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
