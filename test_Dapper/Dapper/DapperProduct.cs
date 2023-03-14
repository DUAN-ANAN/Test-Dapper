using Microsoft.Data.SqlClient;
using test_Dapper.Models;
using Dapper;

namespace test_Dapper.Dapper
{
    public class DapperProduct
    {
        private readonly string _connString = @"Server=localhost\SQLEXPRESS;Database=AdventureWorksLT2019;User Id=sa;Password=andy54088;Trusted_Connection=True;"; //需放到appsetting


        public IEnumerable<Product> GetProduct()
        {
            var sql = @"SELECT * FROM [SalesLT].[Product]";
            using (var conn = new SqlConnection(_connString))
            {
                var result = conn.Query<Product>(sql);
                return result;
            }
        }

        public IEnumerable<Product> GetProductById(string ProductID)
        {
            var sql = @"SELECT * FROM [SalesLT].[Product] WHERE ProductID = @ProductID";
            
            //新增參數對應
            var para = new DynamicParameters();
            para.Add("ProductID", ProductID, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connString))
            {
                var result = conn.Query<Product>(sql, para);
                return result;
            }
        }
    }
}
