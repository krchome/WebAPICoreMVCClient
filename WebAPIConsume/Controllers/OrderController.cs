using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIConsume.Models;

namespace WebAPIConsume.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Order> orderList = new List<Order>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Order"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderList = JsonConvert.DeserializeObject<List<Order>>(apiResponse);
                }
            }
            return View(orderList);
        }

        [HttpGet]
        public ViewResult GetOrder() => View();


        [HttpPost]
        public async Task<IActionResult> GetOrder(int id)
        {
            Order order = new Order();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Order/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(apiResponse);
                }
            }
            
            return View(order);
        }

        [HttpGet]
        public ViewResult AddOrder() => View();

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            //Order receivedOrder = new Order();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:64798/api/Order", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(apiResponse);
                    }
                }
                // ModelState.Clear();
                return View(order);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            Order order = new Order();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Order/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<Order>(apiResponse);
                }
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            Order receivedOrder = new Order();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(order.Id.ToString()), "Id");
                    content.Add(new StringContent(order.CustomerId.ToString()), "CustomerId");
                    content.Add(new StringContent(order.Description), "Description");
                    content.Add(new StringContent(order.OrderCost.ToString()), "OrderCost");

                    using (var response = await httpClient.PutAsync("http://localhost:64798/api/Order", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        receivedOrder = JsonConvert.DeserializeObject<Order>(apiResponse);
                    }
                }
            }
            return View(receivedOrder);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:64798/api/Order/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
