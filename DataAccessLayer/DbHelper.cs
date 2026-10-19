
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccessLayer
{
    public static class DbHelper
    {
        private static readonly string connectionString =
            LoadConnectionString();

        private static string LoadConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: false)
                .Build();

            string? value =
                configuration.GetConnectionString("Supabase");

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    "The Supabase connection string is missing from appsettings.json.");
            }

            return value;
        }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
