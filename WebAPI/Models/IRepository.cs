using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIControllers.Models
{
    public interface IRepository
    {
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservationById(int id);
        Reservation AddReservation(Reservation reservation);
        Reservation UpdateReservation(Reservation reservation);
        void DeleteReservation(int? id);
    }
}
