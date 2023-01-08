namespace Shop.Logic.Services {
    using DataModels.CustomModels;
    using DataModels.Models;
    using MySqlConnector;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class AdminService : IAdminService {

        private readonly static Random Random = new Random();

        private readonly MySqlConnection _dbConnection;

        public AdminService(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ResponseModel AdminLogin(LoginModel loginModel)
        {
            _dbConnection.Open();
            var response = new ResponseModel();

            try{
                var command = new MySqlCommand("SELECT * FROM admins WHERE email = @Email AND password = @Password", connection: _dbConnection);
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

        public bool SaveProducer(ProducerModel newProducer)
        {
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("INSERT INTO producers(Name, Industry) VALUES (@Name, @Industry); COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Name", value: newProducer.Name);
                command.Parameters.AddWithValue("@Industry", newProducer.Industry ?? 0);
                command.ExecuteReader();
                _dbConnection.Close();
                return true;
            }
            catch (Exception){
                _dbConnection.Close();
                return false;
            }
        }

        public bool SaveProduct(ProductModel newProduct)
        {
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("INSERT INTO products(Name, Price, Quantity, ProducerId) VALUES (@Name, @Price, @Quantity, @ProducerId); COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Name", value: newProduct.Name);
                command.Parameters.AddWithValue("@Price", value: newProduct.Price);
                command.Parameters.AddWithValue("@Quantity", value: newProduct.Quantity);
                command.Parameters.AddWithValue("@ProducerId", value: newProduct.ProducerId);
                command.ExecuteReader();
                _dbConnection.Close();
                return true;
            }
            catch (Exception){
                _dbConnection.Close();
                return false;
            }
        }

        public bool UpdateProducer(ProducerModel producerToUpdate)
        {
            var flag = false;
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("UPDATE producers SET Name = @Name, Industry = @Industry WHERE Id = @Id; COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Name", value: producerToUpdate.Name);
                command.Parameters.AddWithValue("@Industry", producerToUpdate.Industry ?? 0);
                command.Parameters.AddWithValue("@Id", value: producerToUpdate.Id);
                command.ExecuteReader();
                flag = true;
            }
            catch (Exception){
                // ignored
            }
            _dbConnection.Close();
            return flag;
        }

        public bool UpdateProduct(ProductModel productToUpdate)
        {
            var flag = false;
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("UPDATE products SET Name = @Name, Price = @Price, Quantity = @Quantity, ProducerId = @ProducerId WHERE Id = @Id; COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Name", value: productToUpdate.Name);
                command.Parameters.AddWithValue("@Price", value: productToUpdate.Price);
                command.Parameters.AddWithValue("@Quantity", value: productToUpdate.Quantity);
                command.Parameters.AddWithValue("@ProducerId", value: productToUpdate.ProducerId);
                command.Parameters.AddWithValue("@Id", value: productToUpdate.Id);
                command.ExecuteReader();
                flag = true;
            }
            catch (Exception){
                // ignored
            }
            _dbConnection.Close();
            return flag;
        }

        public bool DeleteProducer(ProducerModel producerToDelete)
        {
            return Delete(id: producerToDelete.Id, "producers");
        }

        public bool DeleteProduct(ProductModel productToDelete)
        {
            return Delete(id: productToDelete.Id, "products");
        }
        public List<ProducerModel> GetProducers()
        {
            return FetchAll<ProducerModel>("producers");
        }

        public List<ProductModel> GetProducts()
        {
            return FetchAll<ProductModel>("products");
        }
        private bool Delete(int? id, string tableName)
        {
            var flag = false;
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("DELETE FROM " + tableName + " WHERE Id = @Id; COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Id", value: id);
                command.ExecuteReader();
                flag = true;
            }
            catch (Exception){
                // ignored
            }
            _dbConnection.Close();
            return flag;
        }
        private List<T> FetchAll<T>(string tableName)
        {
            var list = new List<T>();
            _dbConnection.Open();
            MySqlDataReader reader = null;
            try{
                var command = new MySqlCommand("SELECT * FROM " + tableName, connection: _dbConnection);
                reader = command.ExecuteReader();

                while (reader.Read()){
                    var obj = (T)Activator.CreateInstance(typeof(T));
                    foreach (var property in obj.GetType().GetProperties()){
                        if (property.Name == "Industry")
                            property.SetValue(obj: obj, (EIndustry)reader[property.Name]);
                        else
                            property.SetValue(obj: obj, reader[property.Name]);
                    }
                    list.Add(obj);
                }
            }
            catch (Exception){
                // ignored
            }
            reader?.Close();
            _dbConnection.Close();
            return list;
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

        private static string GetNewSessionId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(element: chars, count: length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
