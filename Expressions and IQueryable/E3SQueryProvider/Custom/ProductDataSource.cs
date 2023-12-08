using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Expressions.Task3.E3SQueryProvider.Custom;

public class SqlDataSource
{
    private readonly string _connectionString;
    public SqlDataSource(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable ExecuteSqlQuery(string query)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        var reader = command.ExecuteReader();

        var listResult = new List<Product>();
        while (reader.Read())
        {
            listResult.Add(new Product
            {
                ProductId = reader.GetInt32(0),
                ProductName = reader.GetString(1),
                UnitPrice = reader.GetInt32(2),
                ProductType = reader.GetString(3)
            });
        }
        return listResult;
    }
}