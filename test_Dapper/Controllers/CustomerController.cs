using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test_Dapper.Models;
using test_Dapper.Service;

namespace test_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var customers = await _customerService.GetAllCustomerAsync();
            return Ok(customers);
        }

        [HttpGet("{customerID}")]
        public async Task<ActionResult<Customer>> GetAllCustomerById(int customerID)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerID);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCustomer = await _customerService.CreateCustomerAsync(customer);
            return Ok(newCustomer);
        }

        [HttpDelete]
        public async Task<ActionResult<Customer>> DeleteCustomerAsync(int customerID)
        {
            if (customerID <= 0)
            {
                return BadRequest();
            }

            var res = await _customerService.DeleteCustomerAsync(customerID);
            if (res == true)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
