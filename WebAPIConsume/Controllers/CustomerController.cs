using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIConsume.Models;

namespace WebAPIConsume.Controllers
{
    
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Customer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(customerList);
        }
        public ViewResult GetCustomer() => View();
       

        [HttpPost]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Customer/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(customer);
        }
        [HttpGet]
      
        public ViewResult AddCustomer() => View();

        [HttpPost]
        
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:64798/api/Customer", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                }
                return View(customer);
            }
            return View(); 
            //return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Customer/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            Customer receivedCustomer = new Customer();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(customer.Id.ToString()), "Id");
                    content.Add(new StringContent(customer.Name), "Name");
                    content.Add(new StringContent(customer.Address), "Address");
                    content.Add(new StringContent(customer.Telephone), "Telephone");
                    content.Add(new StringContent(customer.Email), "Email");
                    using (var response = await httpClient.PutAsync("http://localhost:64798/api/Customer", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        receivedCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                }
            }
            return View(receivedCustomer);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:64798/api/Customer/" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

    }
}
