namespace Shop.Admin.Services {
    using DataModels.CustomModels;
    using Microsoft.AspNetCore.Components;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AdminPanelService : IAdminPanelService {
        private readonly HttpClient _httpClient;

        public AdminPanelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseModel> AdminLogin(LoginModel loginModel)
        {
            return await _httpClient.PostJsonAsync<ResponseModel>("api/admin/AdminLogin", content: loginModel);
        }
    }
}
