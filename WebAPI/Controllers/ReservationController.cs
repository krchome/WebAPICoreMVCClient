using System;
using System.Collections.Generic;
using WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace APIControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private IRepository repository;

        private IWebHostEnvironment webHostEnvironment;

        public ReservationController(IRepository repo, IWebHostEnvironment environment)
        {
            repository = repo;
            webHostEnvironment = environment;
        }

        //public ReservationController(IRepository repo) => repository = repo;
        [HttpGet]
        public IEnumerable<Reservation> GetReservations()
        {
            return repository.GetAllReservations().ToList();
        }

        [HttpGet("{id}")]
        public Reservation GetReservationById(int id)
        {
            return repository.GetReservationById(id);
        }



        [HttpPost]
        public Reservation Create([FromBody] Reservation res)
        {
            return repository.AddReservation(res);
         }



        [HttpPut]
        public Reservation Update([FromForm] Reservation res)
        {
            return repository.UpdateReservation(res);
        }


        [HttpDelete("{id}")]
        public void Delete(int? id) => repository.DeleteReservation(id);
  
    }
}