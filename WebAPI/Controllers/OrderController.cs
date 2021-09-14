using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Models;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository;

        //private IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository repo)
        {
            orderRepository = repo;
            //_logger = logger;
           // webHostEnvironment = environment;
        }

        //public ReservationController(IRepository repo) => repository = repo;
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return orderRepository.GetAllOrders().ToList();
        }
        [HttpGet("{id}")]
        public Order GetOrderById(int id)
        {
            return orderRepository.GetOrderById(id);
        }
        [HttpPost]
        public Order Create([FromBody] Order order)
        {
            return orderRepository.AddOrder(order);
        }
        [HttpPut]
        public Order Update([FromForm] Order order)
        {
            return orderRepository.UpdateOrder(order);
        }
        [HttpDelete("{id}")]
        public void Delete(int? id) => orderRepository.DeleteOrder(id);
    }
}
