using Shop.DataModels.CustomModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace Shop.Admin.Services {

    public class AdminPanelService : IAdminPanelService {
        private readonly HttpClient _httpClient;

        public AdminPanelService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task<ResponseModel> AdminLogin(LoginModel loginModel)
        {
            return await _httpClient.PostJsonAsync<ResponseModel>("api/admin/AdminLogin", loginModel);
        }
    }
}
