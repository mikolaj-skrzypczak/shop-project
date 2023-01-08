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

        [HttpPost] [Route("SaveProducer")] public IActionResult SaveProducer(ProducerModel newProducer)
        {
            var data = _adminService.SaveProducer(newProducer);
            return Ok(data);
        }

        [HttpGet] [Route("GetProducers")] public IActionResult GetProducers()
        {
            var data = _adminService.GetProducers();
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
    }
}
