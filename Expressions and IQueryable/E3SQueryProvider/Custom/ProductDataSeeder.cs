using System;
using System.Data;
using System.Data.SqlClient;

namespace Expressions.Task3.E3SQueryProvider.Custom;

public class ProductDataSeeder
{
    private readonly string _dbName;
    private readonly string _connectionString;
    private readonly string _masterConnectionString;

    public ProductDataSeeder(string connectionString, string masterConnectionString)
    {
        _dbName = "LINQ_TEST";
        _masterConnectionString = masterConnectionString;
        _connectionString = connectionString;
    }

    public void InitializeDatabase()
    {
        using (var connection = new SqlConnection(_masterConnectionString))
        {
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"CREATE DATABASE {_dbName}";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine($"Database '{_dbName}' already exists.");
            }
        }

        Console.WriteLine($"Database '{_dbName}' created.");
    }

    public void InitializeProductsTable()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                    CREATE TABLE Products (
                        ProductId INT PRIMARY KEY,
                        ProductName NVARCHAR(255),
                        UnitPrice INT,
                        ProductType NVARCHAR(255)
                    )";

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine($"Products '{_dbName}' already exists.");
            }
        }

        Console.WriteLine("Products table created.");
    }

    public void SeedData()
    {
        InitializeDatabase();
        InitializeProductsTable();

        // Seed data
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var products = new[]
            {
                new { ProductId = 1, ProductName = "Product1", UnitPrice = 150, ProductType = "Customized Product" },
                new { ProductId = 2, ProductName = "Product2", UnitPrice = 80, ProductType = "Regular Product" },
            };

            foreach (var product in products)
            {
                using var command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Products (ProductId, ProductName, UnitPrice, ProductType) " +
                                      "VALUES (@ProductId, @ProductName, @UnitPrice, @ProductType)";

                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@ProductType", product.ProductType);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Seed has been added.");
                }
            }
        }

        Console.WriteLine("Data seeding completed.");
    }
}
