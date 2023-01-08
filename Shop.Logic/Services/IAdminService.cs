namespace Shop.Logic.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using System.Collections.Generic;

    public interface IAdminService {
        ResponseModel AdminLogin(LoginModel loginModel);
        List<ProducerModel> GetProducers();
        bool SaveProducer(ProducerModel newProducer);
        bool UpdateProducer(ProducerModel producerToUpdate);
        bool DeleteProducer(ProducerModel producerToDelete);
        List<ProductModel> GetProducts();
        bool SaveProduct(ProductModel newProduct);
        bool UpdateProduct(ProductModel productToUpdate);
        bool DeleteProduct(ProductModel productToDelete);
    }
}
