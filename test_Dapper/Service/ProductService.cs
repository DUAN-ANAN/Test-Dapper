using Dapper;
using Microsoft.Data.SqlClient;
using test_Dapper.Models;

namespace test_Dapper.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int Id);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
    }
    /*public class ProductService : IProductService
    {

        private readonly string _connString = @"Server=localhost\SQLEXPRESS;Database=AdventureWorksLT2019;User Id=sa;Password=andy54088;Trusted_Connection=True;"; //需放到appsetting


        public async Task<List<Product>> GetAllProductAsync()
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM [SalesLT].[Product]";
                return (List<Product>)await conn.QueryAsync<Product>(query);
            }
        }

        public async Task<Product> GetProductByIdAsync(int ProductId)
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM [SalesLT].[Product] WHERE ProductId = @ProductId";
                var para = new DynamicParameters();
                para.Add("ProductId", ProductId, System.Data.DbType.Int32);
                return (Product)await conn.QueryAsync<Product>(query, para);
            }
        }

        //public async Task<bool> CreateProductAsync(Product product)
        //{
        //    using (var conn = new SqlConnection(_connString))
        //    {
        //        await conn.OpenAsync();
        //        var query = @"INSERT INTO [SalesLT].[Product] () VALUES ();";
        //        var para = new DynamicParameters();
        //        para.Add("ProductId", ProductId, System.Data.DbType.Int32);
                
                
                
                
        //        return;
        //    }
        //}




    }*/
}
