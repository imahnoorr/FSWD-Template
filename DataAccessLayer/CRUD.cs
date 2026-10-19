
using System;
using System.Collections.Generic;
using Npgsql;
using Models;

namespace DataAccessLayer
{
    public class CRUD
    {
        public static int TestConnection()
        {
            using (NpgsqlConnection connection = DbHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand("SELECT 1", connection))
                {
                    cmd.CommandTimeout = 15;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static List<ProductModel> GetAllData()
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                using (NpgsqlConnection connection = DbHelper.GetConnection())
                {
                    connection.Open();

                    string query =
                        "SELECT \"Pid\", \"ProductName\", \"Price\" " +
                        "FROM public.product ORDER BY \"Pid\"";

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 15;

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductModel product = new ProductModel
                                {
                                    PId = Convert.ToInt32(reader["Pid"]),
                                    ProductName =
                                        Convert.ToString(reader["ProductName"]) ?? "",
                                    Price = Convert.ToInt32(reader["Price"])
                                };

                                products.Add(product);
                            }
                        }
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    "GetAllData failed: " + ex);
                throw;
            }
        }

        public static int InsertData(ProductModel model)
        {
            string query =
                "INSERT INTO public.product " +
                "(\"Pid\", \"ProductName\", \"Price\") " +
                "VALUES (@Pid, @ProductName, @Price)";

            try
            {
                using (NpgsqlConnection connection = DbHelper.GetConnection())
                {
                    connection.Open();

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 15;
                        cmd.Parameters.AddWithValue("@Pid", model.PId);
                        cmd.Parameters.AddWithValue(
                            "@ProductName", model.ProductName);
                        cmd.Parameters.AddWithValue("@Price", model.Price);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("InsertData failed: " + ex);
                throw;
            }
        }

        public static int UpdateData(ProductModel model)
        {
            string query =
                "UPDATE public.product " +
                "SET \"ProductName\" = @ProductName, \"Price\" = @Price " +
                "WHERE \"Pid\" = @Pid";

            try
            {
                using (NpgsqlConnection connection = DbHelper.GetConnection())
                {
                    connection.Open();

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 15;
                        cmd.Parameters.AddWithValue("@Pid", model.PId);
                        cmd.Parameters.AddWithValue(
                            "@ProductName", model.ProductName);
                        cmd.Parameters.AddWithValue("@Price", model.Price);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("UpdateData failed: " + ex);
                throw;
            }
        }

        public static int DeleteData(int id)
        {
            string query =
                "DELETE FROM public.product WHERE \"Pid\" = @Pid";

            try
            {
                using (NpgsqlConnection connection = DbHelper.GetConnection())
                {
                    connection.Open();

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 15;
                        cmd.Parameters.AddWithValue("@Pid", id);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("DeleteData failed: " + ex);
                throw;
            }
        }
    }
}
