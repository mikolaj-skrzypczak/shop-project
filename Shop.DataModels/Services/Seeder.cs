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
            SeedProducer();
            _dbConnection.Close();
        }

        private void SeedAdmin()
        {
            var query = @"
                DROP TABLE IF EXISTS admins;
                CREATE TABLE admins (
                    Id int NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(100) NULL,Email VARCHAR(100) NOT NULL UNIQUE,
                    Password VARCHAR(255) NOT NULL,
                    PRIMARY KEY (Id));
                INSERT INTO admins VALUES (1, 'Admin', 'admin@admin.com', @password);";

            var command = new MySqlCommand(commandText: query, connection: _dbConnection);
            command.Parameters.AddWithValue("@password", CreateMd5("admin"));

            var reader = command.ExecuteReader();
            reader.Close();
        }

        private void SeedProducer()
        {
            var query = @"
                DROP TABLE IF EXISTS producers;
                CREATE TABLE producers (
                    Id int NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(100) NOT NULL UNIQUE,
                    Industry INT NULL,
                    PRIMARY KEY (Id));
                INSERT INTO producers(Name, Industry) VALUES
                    ('Leroy Merlin', 2),
                    ('PepsiCo, Inc.', 6),
                    ('Lamborghini', 1);";
            var command = new MySqlCommand(commandText: query, connection: _dbConnection);
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
