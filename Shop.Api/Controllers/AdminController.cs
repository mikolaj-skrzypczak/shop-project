namespace Shop.Api.Controllers {
    using DataModels.CustomModels;
    using DataModels.Models;
    using Logic.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {

        private readonly IAdminService _adminService;

        private readonly IWebHostEnvironment _env;

        public AdminController(IAdminService adminService, IWebHostEnvironment env)
        {
            _env = env;
            _adminService = adminService;
        }

        [HttpPost] [Route("AdminLogin")] public IActionResult AdminLogin(LoginModel loginModel)
        {
            var data = _adminService.AdminLogin(loginModel);
            return Ok(data);
        }

        [HttpGet] [Route("GetProducers")] public IActionResult GetProducers()
        {
            var data = _adminService.GetProducers();
            return Ok(data);
        }

        [HttpPost] [Route("SaveProducer")] public IActionResult SaveProducer(ProducerModel newProducer)
        {
            var data = _adminService.SaveProducer(newProducer);
            return Ok(data);
        }

        [HttpPost] [Route("UpdateProducer")] public IActionResult UpdateProducer(ProducerModel producerToUpdate)
        {
            var data = _adminService.UpdateProducer(producerToUpdate);
            return Ok(data);
        }

        [HttpPost] [Route("DeleteProducer")] public IActionResult DeleteProducer(ProducerModel producerToDelete)
        {
            var data = _adminService.DeleteProducer(producerToDelete);
            return Ok(data);
        }

        [HttpGet] [Route("GetProducts")] public IActionResult GetProducts()
        {
            var data = _adminService.GetProducts();
            return Ok(data);
        }

        [HttpPost] [Route("SaveProduct")] public IActionResult SaveProduct(ProductModel newProduct)
        {
            var data = _adminService.SaveProduct(newProduct);
            return Ok(data);
        }

        [HttpPost] [Route("UpdateProduct")] public IActionResult UpdateProduct(ProductModel productToUpdate)
        {
            var data = _adminService.UpdateProduct(productToUpdate);
            return Ok(data);
        }

        [HttpPost] [Route("DeleteProduct")] public IActionResult DeleteProduct(ProductModel productToDelete)
        {
            var data = _adminService.DeleteProduct(productToDelete);
            return Ok(data);
        }
    }
}
