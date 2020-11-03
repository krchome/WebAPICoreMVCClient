using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository customerRepository;

        private IWebHostEnvironment webHostEnvironment;

        public CustomerController(ICustomerRepository repo, IWebHostEnvironment environment)
        {
            customerRepository = repo;
            webHostEnvironment = environment;
        }

        //public ReservationController(IRepository repo) => repository = repo;
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepository.GetAllCustomers().ToList();
        }

        [HttpGet("{id}")]
        public Customer GetCustomerById(int id)
        {
            return customerRepository.GetCustomerById(id);
        }



        [HttpPost]
        public Customer Create([FromBody] Customer customer)
        {
            return customerRepository.AddCustomer(customer);
        }



        [HttpPut]
        public Customer Update([FromForm] Customer customer)
        {
            return customerRepository.UpdateCustomer(customer);
        }


        [HttpDelete("{id}")]
        public void Delete(int? id) => customerRepository.DeleteCustomer(id);

    }
}
