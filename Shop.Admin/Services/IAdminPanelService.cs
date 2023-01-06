namespace Shop.Admin.Services {
    using DataModels.CustomModels;
    using System.Threading.Tasks;

    public interface IAdminPanelService {
        Task<ResponseModel> AdminLogin(LoginModel loginModel);
    }
}
