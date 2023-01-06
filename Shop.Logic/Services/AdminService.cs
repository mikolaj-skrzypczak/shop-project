using Shop.DataModels.CustomModels;
using Shop.DataModels.Models;
using System;
using System.Linq;
using MySqlConnector;
using System.Security.Cryptography;

namespace Shop.Logic.Services {
    using System.Text;

    public class AdminService : IAdminService {
        private readonly MySqlConnection _dbConnection;

        public AdminService(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public static string CreateMD5(string input)
        {
            string result;
            using (MD5 hash = MD5.Create()){
                result = String.Join(
                "",
                from ba in hash.ComputeHash
                (
                Encoding.UTF8.GetBytes(input)
                )
                select ba.ToString("x2")
                );
            }
            return result;
        }
        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            _dbConnection.Open();
            ResponseModel response = new ResponseModel();

            try{
                var command = new MySqlCommand("SELECT * FROM admininfo WHERE email = @Email AND password = @Password", _dbConnection);
                command.Parameters.AddWithValue("@Email", loginModel.EmailId);
                command.Parameters.AddWithValue("@Password", CreateMD5(loginModel.Password));

                var reader = command.ExecuteReader();

                if (reader.HasRows){
                    response.Status = true;
                    response.Message = "Login Successful";
                }
                else{
                    response.Status = false;
                    response.Message = "Invalid Credentials";
                }
                reader.Close();
                // string email = null;
                // string password = null;
                //     while (reader.Read()){
                //         email = reader.GetString("email");
                //     }
                //     reader.Close();
                //     if (email != null){
                //         response.Status = true;
                //         response.Message = email;
                //     }
                //     else{
                //         response.Status = false;
                //         response.Message = "Invalid Email or Password";
                //     }
            }
            catch (Exception ex){
                response.Message = ex.Message;
                response.Status = false;
            }
            _dbConnection.Close();
            return response;
        }
    }
}
