using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;

namespace DapperEdu.Models
{
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository()
        {
            connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DapperDb;Trusted_Connection=true;MultipleActiveResultSets=true";
        }

        public IDbConnection Connection
        { 
            get 
            {
                return new SqlConnection(connectionString);
            } 
        }  

        public void Add (Product prod)
        {
            using (IDbConnection dbConnection=Connection)
            {
                string sQuery = @"INSERT INTO Products (Name,Quantity,Price) VALUES (@Name,@Quantity,@Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, prod);
            }
        }

        public IEnumerable<Product> GetAll () 
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"Select * FROM Products";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery);
            }     
        }

        public Product GetById(int id) 
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"Select * FROM Products WHERE ProductId=@Id";
                dbConnection.Open();
                return dbConnection.Query<Product>(sQuery, new {Id=id}).FirstOrDefault();    
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"DELETE FROM Products Where ProductId=@Id";
                dbConnection.Open();    
                dbConnection.Execute(sQuery, new {Id=id});
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE Products SET Name=@Name,Quantity=@Quantity,Price=@Price Where ProductId=@ProductId";
                dbConnection.Open();    
                dbConnection.Query(sQuery, prod);
            }
            
        }
    }
}
