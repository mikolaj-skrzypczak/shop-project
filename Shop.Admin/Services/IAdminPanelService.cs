namespace Shop.Admin.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminPanelService {
        Task<ResponseModel> AdminLogin(LoginModel loginModel);
        Task<List<ProducerModel>> GetProducers();
        Task<bool> SaveProducer(ProducerModel newProducer);
        Task<bool> UpdateProducer(ProducerModel producerToUpdate);
        Task<bool> DeleteProducer(ProducerModel producerToDelete);
        Task<List<ProductModel>> GetProducts();
        Task<bool> SaveProduct(ProductModel newProduct);
        Task<bool> UpdateProduct(ProductModel productToUpdate);
        Task<bool> DeleteProduct(ProductModel productToDelete);
    }
}
