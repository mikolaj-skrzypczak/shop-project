namespace Shop.DataModels.CustomModels {
    using System.ComponentModel.DataAnnotations;

    public class LoginModel {
        public string UserKey { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "*EmailId is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "*Password is required")]
        public string Password { get; set; }
    }
}
