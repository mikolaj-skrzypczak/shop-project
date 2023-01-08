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
            DropTables();
            SeedAdmin();
            SeedProducer();
            SeedProducts();
            _dbConnection.Close();
        }

        private void DropTables()
        {
            const string sql = @"
                DROP TABLE IF EXISTS `products`;
                DROP TABLE IF EXISTS `producers`;
                DROP TABLE IF EXISTS `admins`;";
            var command = new MySqlCommand(commandText: sql, connection: _dbConnection);
            var reader = command.ExecuteReader();
            reader.Close();
        }

        private void SeedAdmin()
        {
            const string query = @"
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
            const string query = @"
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

        private void SeedProducts()
        {
            const string query = @"
                CREATE TABLE Products (
                    Id int NOT NULL AUTO_INCREMENT,
                    Name varchar(100) NOT NULL,
                    Price decimal(10,2) NOT NULL,
                    Quantity int NOT NULL,
                    ProducerId int NOT NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (ProducerId) REFERENCES producers(Id) ON DELETE CASCADE);
                INSERT INTO Products(Name, Price, Quantity, ProducerId) VALUES
                    ('Lamborghini Huracan', 1000000, 1, 3),
                    ('Lamborghini Gallardo', 700000, 2, 3),
                    ('Pepsi', 2.5, 259, 2),
                    ('Screwdriver', 10, 1000, 1);";
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
