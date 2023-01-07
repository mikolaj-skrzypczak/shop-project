﻿namespace Shop.Logic.Services {
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

        private readonly string[] _allowedTables = { "admins", "producers", "products" };

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

        public ProducerModel SaveProducer(ProducerModel newProducer)
        {
            _dbConnection.Open();
            try{
                var command = new MySqlCommand("INSERT INTO producers(Name, Industry) VALUES (@Name, @Industry); COMMIT;", connection: _dbConnection);
                command.Parameters.AddWithValue("@Name", value: newProducer.Name);
                command.Parameters.AddWithValue("@Industry", value: newProducer.Industry);

                command.ExecuteReader();
                return newProducer;
            }
            catch (Exception){
                return null;
            }
        }

        public List<ProducerModel> GetProducers()
        {
            return FetchAll<ProducerModel>("producers");
        }
        private List<T> FetchAll<T>(string tableName)
        {
            var list = new List<T>();
            _dbConnection.Open();
            try{
                if (!(Array.IndexOf(array: _allowedTables, value: tableName) > -1)) throw new Exception("No sql injection here :)");
                var command = new MySqlCommand("SELECT * FROM " + tableName, connection: _dbConnection);
                var reader = command.ExecuteReader();

                while (reader.Read()){
                    var obj = (T)Activator.CreateInstance(typeof(T));
                    foreach (var property in obj.GetType().GetProperties()){
                        var tmp = reader[property.Name];

                        if (property.Name == "Industry")
                            property.SetValue(obj: obj, (EIndustry)reader[property.Name]);
                        else
                            property.SetValue(obj: obj, reader[property.Name]);

                    }
                    list.Add(obj);
                }
                reader.Close();
            }
            catch (Exception ex){
                var exc = ex;
                return list;
            }
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


        public static string GetNewSessionId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(element: chars, count: length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}