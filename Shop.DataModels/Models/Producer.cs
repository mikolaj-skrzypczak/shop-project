#nullable enable
namespace Shop.DataModels.Models {
    using System.ComponentModel.DataAnnotations;

    public class ProducerModel {
        public int? Id { get; set; }

        [Required(ErrorMessage = "*Producer name is required")]
        public string? Name { get; set; }

        public EIndustry? Industry { get; set; }
    }
}
