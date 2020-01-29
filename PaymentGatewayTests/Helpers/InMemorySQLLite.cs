using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Repository;
using PaymentGateway.Repository.Models;
using System;
using System.Linq;

namespace PaymentGatewayTests.Helpers
{
    class InMemorySQLLite
    {
        public static DbContextOptions<T> CreateOptions<T>()
            where T : DbContext
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlite(connection);

            return builder.Options;
        }
    }
}
