namespace Shop.Admin.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminPanelService {
        Task<ResponseModel> AdminLogin(LoginModel loginModel);
        Task<bool> SaveProducer(ProducerModel newProducer);
        Task<List<ProducerModel>> GetProducers();
        Task<bool> UpdateProducer(ProducerModel producerToUpdate);
        Task<bool> DeleteProducer(ProducerModel producerToDelete);
    }
}
