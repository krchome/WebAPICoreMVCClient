using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Reservation"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        public ViewResult GetReservation() => View();

        [HttpPost]
        public async Task<IActionResult> GetReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }
        [HttpGet]
        public ViewResult AddReservation() => View();

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:64798/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

        /*[HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@1231");
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:64798/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                }
            }
            return View(receivedReservation);
        }*/
        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:64798/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.Id.ToString()), "Id");
                content.Add(new StringContent(reservation.Name), "Name");
                content.Add(new StringContent(reservation.StartLocation), "StartLocation");
                content.Add(new StringContent(reservation.EndLocation), "EndLocation");

                using (var response = await httpClient.PutAsync("http://localhost:64798/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

        //public async Task<IActionResult> UpdateReservationPatch(int id)
        //{
        //    Reservation reservation = new Reservation();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("http://localhost:64798/api/Reservation/" + id))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //        }
        //    }
        //    return View(reservation);
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdateReservationPatch(int id, Reservation reservation)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var request = new HttpRequestMessage
        //        {
        //            RequestUri = new Uri("http://localhost:64798/api/Reservation/" + id),
        //            Method = new HttpMethod("Patch"),
        //            Content = new StringContent("[{ \"op\":\"replace\", \"path\":\"Name\", \"value\":\"" + reservation.Name + "\"},{ \"op\":\"replace\", \"path\":\"StartLocation\", \"value\":\"" + reservation.StartLocation + "\"}]", Encoding.UTF8, "application/json")
        //        };

        //        var response = await httpClient.SendAsync(request);
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:64798/api/Reservation/" + ReservationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public ViewResult AddFile() => View();

        //[HttpPost]
        //public async Task<IActionResult> AddFile(IFormFile file)
        //{
        //    string apiResponse = "";
        //    using (var httpClient = new HttpClient())
        //    {
        //        var form = new MultipartFormDataContent();
        //        using (var fileStream = file.OpenReadStream())
        //        {
        //            form.Add(new StreamContent(fileStream), "file", file.FileName);
        //            using (var response = await httpClient.PostAsync("http://localhost:64798/api/Reservation/UploadFile", form))
        //            {
        //                response.EnsureSuccessStatusCode();
        //                apiResponse = await response.Content.ReadAsStringAsync();
        //            }
        //        }
        //    }
        //    return View((object)apiResponse);
        //}

        //public ViewResult AddReservationByXml() => View();

        //[HttpPost]
        //public async Task<IActionResult> AddReservationByXml(Reservation reservation)
        //{
        //    Reservation receivedReservation = new Reservation();

        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(ConvertObjectToXMLString(reservation), Encoding.UTF8, "application/xml");

        //        using (var response = await httpClient.PostAsync("http://localhost:64798/api/Reservation/PostXml", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
        //        }
        //    }
        //    return View(receivedReservation);
        //}

        //string ConvertObjectToXMLString(object classObject)
        //{
        //    string xmlString = null;
        //    XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        xmlSerializer.Serialize(memoryStream, classObject);
        //        memoryStream.Position = 0;
        //        xmlString = new StreamReader(memoryStream).ReadToEnd();
        //    }
        //    return xmlString;
        //}
    }
}