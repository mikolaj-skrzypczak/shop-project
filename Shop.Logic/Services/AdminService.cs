namespace Shop.Logic.Services {
    using DataModels.CustomModels;
    using MySqlConnector;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class AdminService : IAdminService {
        private readonly MySqlConnection _dbConnection;

        private readonly static Random Random = new Random();


        public AdminService(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            _dbConnection.Open();
            var response = new ResponseModel();

            try{
                var command = new MySqlCommand("SELECT * FROM admininfo WHERE email = @Email AND password = @Password", connection: _dbConnection);
                command.Parameters.AddWithValue("@Email", value: loginModel.EmailId);
                command.Parameters.AddWithValue("@Password", CreateMd5(loginModel.Password));

                var reader = command.ExecuteReader();

                if (reader.HasRows){
                    response.Status = true;
                    response.Message = GetNewSessionId(12);
                }
                else{
                    response.Status = false;
                    response.Message = "Invalid Credentials";
                }
                reader.Close();
            }
            catch (Exception ex){
                response.Message = ex.Message;
                response.Status = false;
            }
            _dbConnection.Close();
            return response;
        }
        private static string CreateMd5(string input)
        {
            string result;
            using (var hash = MD5.Create()){
                result = string.Join(
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


        public static string GetNewSessionId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
