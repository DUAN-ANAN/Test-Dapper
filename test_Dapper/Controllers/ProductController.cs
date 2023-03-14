using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test_Dapper.Dapper;
using test_Dapper.Models;

namespace test_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<Product> Product()
        {
            var d = new DapperProduct();
            var r = d.GetProduct();
            return r;
        }

        //[HttpGet("GetProductById/{ProductID}")] // 指定路由名稱
        [HttpGet("[action]/{ProductID}")] // 會對應下面的action name --> appl
        public IEnumerable<Product> Product(string ProductID)
        {
            var d = new DapperProduct();
            var r = d.GetProductById(ProductID);
            return r;
        }
    }
}
