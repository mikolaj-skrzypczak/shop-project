namespace Shop.DataModels.Models {
    using System.ComponentModel.DataAnnotations;

    public class ProductModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "*Product name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Product price is required")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "*Product quantity is required")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "*Product Producer is required")]
        public int ProducerId { get; set; }
    }
}
