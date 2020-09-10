using ADONETConnected.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADONETConnected.DataAccess
{
    public class ProductDAL
    {
        private int rowsAffected { get; set; }
        public string ResultText { get; set; }

        public void InsertProduct(Product product)
        {
            rowsAffected = 0;

            // Create SQL statement to submit
            string sql = "INSERT INTO Product(ProductId, ProductName, IntroductionDate, Url, Price)";
            sql += $" VALUES({product.ProductId}, '{product.ProductName}', '{product.IntroductionDate.ToString("yyyy-MM-dd")}', '{product.Url}', {product.Price})";

            try
            {
                // Create SQL connection object in using block for automatic closing and disposing
                using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
                {
                    // Create command object in using block for automatic disposal
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        // Set CommandType
                        cmd.CommandType = CommandType.Text;
                        // Open the connection
                        cnn.Open();
                        // Execute the INSERT statement
                        rowsAffected = cmd.ExecuteNonQuery();

                        ResultText = "Rows Affected: " + rowsAffected.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResultText = ex.ToString();
            }
        }

        public int GetProductsCountScalar()
        {
            rowsAffected = 0;
            // Create SQL statement to submit
            string sql = "SELECT COUNT(*) FROM Product";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Open the connection
                    cnn.Open();
                    // Execute command
                    rowsAffected = (int)cmd.ExecuteScalar();
                }
            }
            ResultText = "Rows Affected: " + rowsAffected.ToString();

            return rowsAffected;
        }

        public List<Product> GetProductsAsGenericList()
        {
            List<Product> products = new List<Product>();

            // Create SQL statement to submit
            string sql = "SELECT ProductId, ProductName, IntroductionDate, Url, Price FROM Product";

            // Create a connection
            using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
            {
                // Create command object
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    // Open the connection
                    cnn.Open();

                    // Execute command to get data reader
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = dr.GetInt32(dr.GetOrdinal("ProductId")),
                                ProductName = dr.GetString(dr.GetOrdinal("ProductName")),
                                IntroductionDate = dr.GetDateTime(dr.GetOrdinal("IntroductionDate")),
                                Url = dr.GetString(dr.GetOrdinal("Url")),
                                Price = dr.GetDecimal(dr.GetOrdinal("Price"))
                            });
                        }
                    }
                }
            }

            return products;
        }
    }
}