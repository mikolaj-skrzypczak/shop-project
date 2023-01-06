namespace Shop.DataModels.Services {
    using MySqlConnector;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Seeder : ISeeder {
        private readonly MySqlConnection _dbConnection;

        public Seeder(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Seed()
        {
            _dbConnection.Open();
            SeedAdmin();
            _dbConnection.Close();
        }

        private void SeedAdmin()
        {
            var query = @"
                DROP TABLE IF EXISTS admininfo;
                CREATE TABLE admininfo (
                    Id int NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(100) NULL,Email VARCHAR(100) NOT NULL UNIQUE,
                    Password VARCHAR(255) NOT NULL,
                    PRIMARY KEY (Id));
                INSERT INTO AdminInfo VALUES (1, 'Admin', 'admin@admin.com', @password);";

            var command = new MySqlCommand(query, _dbConnection);
            command.Parameters.AddWithValue("@password", CreateMd5("admin"));

            var reader = command.ExecuteReader();
            reader.Close();
        }

        private static string CreateMd5(string input)
        {
            using var hash = MD5.Create();
            var result = string.Join(
            "",
            from ba in hash.ComputeHash
            (
            Encoding.UTF8.GetBytes(input)
            )
            select ba.ToString("x2")
            );
            return result;
        }
    }
}
