namespace Shop.Logic.Services {
    using DataModels.CustomModels;

    public interface IAdminService {
        ResponseModel AdminLogin(LoginModel loginModel);
    }
}
