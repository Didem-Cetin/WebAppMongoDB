namespace Food_WebAppMongoDB.Models
{
    public class FoodCreateViewModel
    {

        public string FoodName { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

    public class FoodEditViewModel
    {
        public string FoodName { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
