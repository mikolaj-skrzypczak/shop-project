namespace Shop.Admin.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
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

        public async Task<ProducerModel> SaveProducer(ProducerModel newProducer)
        {
            return await _httpClient.PostJsonAsync<ProducerModel>("api/admin/SaveProducer", content: newProducer);
        }

        public async Task<List<ProducerModel>> GetProducers()
        {
            return await _httpClient.GetJsonAsync<List<ProducerModel>>("api/admin/GetProducers");
        }
    }
}
