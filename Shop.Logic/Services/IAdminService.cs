using Shop.DataModels.CustomModels;

namespace Shop.Logic.Services {

    public interface IAdminService {
        ResponseModel AdminLogin(LoginModel loginModel);
    }
}
