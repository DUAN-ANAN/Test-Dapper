using Dapper;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using test_Dapper.Models;

namespace test_Dapper.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int Id);
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<int> CreateCustomerReturnIdAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int customerID);
        //Task<bool> UpdateCustomerAsync(Customer customer); //懶惰實作==
    }


    public class CustomerService : ICustomerService
    {
        private readonly string _connString = @"Server=localhost\SQLEXPRESS;Database=AdventureWorksLT2019;User Id=sa;Password=andy54088;Trusted_Connection=True;"; //需放到appsetting

        /// <summary>
        /// 取得全部Customer
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM [SalesLT].[Customer];";
                return (List<Customer>)await conn.QueryAsync<Customer>(query);
            }
        }

        /// <summary>
        /// 根據CustomerId取得Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByIdAsync(int customerID)
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = "SELECT * FROM [SalesLT].[Customer] WHERE CustomerID = @CustomerID";
                var para = new DynamicParameters();
                para.Add("CustomerID", customerID, System.Data.DbType.Int32);

                var res = await conn.QueryAsync<Customer>(query, para);

                //var res = (Customer)await conn.QueryAsync<Customer>(query, para);
                return res.SingleOrDefault();
            }
        }

        /// <summary>
        /// 新增Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO [SalesLT].[Customer]
           ([NameStyle]
           ,[Title]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[Suffix]
           ,[CompanyName]
           ,[SalesPerson]
           ,[EmailAddress]
           ,[Phone]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[rowguid]
           ,[ModifiedDate])
     VALUES
           (@NameStyle
           ,@Title
           ,@FirstName
           ,@MiddleName
           ,@LastName
           ,@Suffix
           ,@CompanyName
           ,@SalesPerson
           ,@EmailAddress
           ,@Phone
           ,@PasswordHash
           ,@PasswordSalt
           ,@rowguid
           ,@ModifiedDate)";
                #region para
                var para = new DynamicParameters();
                para.Add("NameStyle", customer.NameStyle, System.Data.DbType.Boolean);
                para.Add("Title", customer.Title, System.Data.DbType.String);
                para.Add("FirstName", customer.FirstName, System.Data.DbType.String);
                para.Add("MiddleName", customer.MiddleName, System.Data.DbType.String);
                para.Add("LastName", customer.LastName, System.Data.DbType.String);
                para.Add("Suffix", customer.Suffix, System.Data.DbType.String);
                para.Add("CompanyName", customer.CompanyName, System.Data.DbType.String);
                para.Add("SalesPerson", customer.SalesPerson, System.Data.DbType.String);
                para.Add("EmailAddress", customer.EmailAddress, System.Data.DbType.String);
                para.Add("Phone", customer.Phone, System.Data.DbType.String);
                para.Add("PasswordHash", customer.PasswordHash, System.Data.DbType.String);
                para.Add("PasswordSalt", customer.PasswordSalt, System.Data.DbType.String);
                para.Add("rowguid", customer.rowguid, System.Data.DbType.Guid);
                para.Add("ModifiedDate", customer.ModifiedDate, System.Data.DbType.DateTime);
                #endregion

                return await conn.ExecuteAsync(query, para) > 1;
            }
        }

        /// <summary>
        /// 新增Customer, 並返還CustomerId
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<int> CreateCustomerReturnIdAsync(Customer customer)
        {
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO [SalesLT].[Customer]
           ([NameStyle]
           ,[Title]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[Suffix]
           ,[CompanyName]
           ,[SalesPerson]
           ,[EmailAddress]
           ,[Phone]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[rowguid]
           ,[ModifiedDate])
     VALUES
           (@NameStyle
           ,@Title
           ,@FirstName
           ,@MiddleName
           ,@LastName
           ,@Suffix
           ,@CompanyName
           ,@SalesPerson
           ,@EmailAddress
           ,@Phone
           ,@PasswordHash
           ,@PasswordSalt
           ,@rowguid
           ,@ModifiedDate)";
                #region para
                var para = new DynamicParameters();
                para.Add("NameStyle", customer.NameStyle, System.Data.DbType.Boolean);
                para.Add("Title", customer.Title, System.Data.DbType.String);
                para.Add("FirstName", customer.FirstName, System.Data.DbType.String);
                para.Add("MiddleName", customer.MiddleName, System.Data.DbType.String);
                para.Add("LastName", customer.LastName, System.Data.DbType.String);
                para.Add("Suffix", customer.Suffix, System.Data.DbType.String);
                para.Add("CompanyName", customer.CompanyName, System.Data.DbType.String);
                para.Add("SalesPerson", customer.SalesPerson, System.Data.DbType.String);
                para.Add("EmailAddress", customer.EmailAddress, System.Data.DbType.String);
                para.Add("Phone", customer.Phone, System.Data.DbType.String);
                para.Add("PasswordHash", customer.PasswordHash, System.Data.DbType.String);
                para.Add("PasswordSalt", customer.PasswordSalt, System.Data.DbType.String);
                para.Add("rowguid", customer.rowguid, System.Data.DbType.Guid);
                para.Add("ModifiedDate", customer.ModifiedDate, System.Data.DbType.DateTime);
                #endregion

                var res = await conn.QueryAsync<int>(query, para);
                int id = res.Single();
                return id;
            }
        }

        /// <summary>
        /// 刪除Customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCustomerAsync(int customerID)
        {
            bool flag = false;
            using (var conn = new SqlConnection(_connString))
            {
                await conn.OpenAsync();
                //先check
                //也可以用 IF EXISTS ....就好
                var para = new DynamicParameters();
                var query = @"SELECT CustomerID FROM [SalesLT].[Customer] WHERE CustomerID = @CustomerID;";
                para.Add("CustomerID", customerID, System.Data.DbType.Int32);
                var res = await conn.QueryAsync<Customer>(query, para);
                var existingCustomer = res.Single();
                if (existingCustomer.CustomerId != 0)
                {
                    var sub_query = @"DELETE [SalesLT].[Customer] WHERE CustomerID = @CustomerID";
                    var sub_para = new DynamicParameters();
                    sub_para.Add("CustomerID", customerID, System.Data.DbType.Int32);
                    flag = await conn.ExecuteAsync(sub_query, sub_para) > 0;
                }
            }
            return flag;
        }
    }
}
