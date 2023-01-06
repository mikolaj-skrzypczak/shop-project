namespace Shop.Api.Controllers {
    using DataModels.CustomModels;
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
    }
}
