using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DAL.TDG.models;


namespace DAL.TDG
{
    public class ProductGateWay
    {
        private string connectionString;

        public ProductGateWay(string connection)
        {
            connectionString = connection;
        }


        public IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM Products";
            ICollection<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ICollection<Provider> providers = new List<Provider>();
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    double price = reader.GetDouble(2);
                    int? categoryId = reader.GetInt32(3);
                    providers = GetProviders(id);
                    products.Add(new Product
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        Providers = providers
                    });

                }
            }
            return products;            
        }

        public Product Get(int? id)
        {
            string query = "SELECT * FROM Products WHERE Products.Id = @id";
            Product prod = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prod = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        CategoryId = reader.GetInt32(3)
                    };
                    
                }
                
            }
            return prod;
        }
        private ICollection<Provider> GetProviders(int id)
        {
            string query = String.Format("SELECT Providers.Id,Providers.Name,Providers.Adress " +
                "FROM Providers, ProviderProducts " +
                "WHERE ProviderProducts.Product_Id = {0} " +
                "AND ProviderProducts.Provider_Id = Providers.Id",id);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ICollection<Provider> providers = new List<Provider>();
                while (reader.Read())
                {
                    int _id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string adress = reader.GetString(2);

                    providers.Add(new Provider
                    {
                        Id = _id,
                        Name = name,
                        Adress = adress
                    });
                }
                return providers;
            }
        }
    }
}