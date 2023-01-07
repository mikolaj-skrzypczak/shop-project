namespace Shop.Logic.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using System.Collections.Generic;

    public interface IAdminService {
        ResponseModel AdminLogin(LoginModel loginModel);
        ProducerModel SaveProducer(ProducerModel newProducer);

        List<ProducerModel> GetProducers();
    }
}
