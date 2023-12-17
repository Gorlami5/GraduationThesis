using Microsoft.EntityFrameworkCore;
using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit
{
    public class ReservationDataAccess :IReservationDataAccess
    {
        private readonly IReservationDataAccess _reservationDataAccess;
        private readonly PostgreDbConnection _connection;
        public ReservationDataAccess(IReservationDataAccess reservationDataAccess, PostgreDbConnection postgreDbConnection)
        {
            _reservationDataAccess = reservationDataAccess;
            _connection = postgreDbConnection;
        }

        public int Add(Reservation reservation)
        {
            _connection.Add(reservation);
            return _connection.SaveChanges();
        }
        public int Delete(Reservation reservation)
        {
            _connection.Remove(reservation);
            return _connection.SaveChanges();
        }
        public int Update(Reservation reservationCompany)
        {
            _connection.Update(reservationCompany);
            return _connection.SaveChanges();
        }
        public Reservation GetReservationById(int reservationId)
        {
            var reservation = _connection.Reservations.Where(c => c.Id == reservationId).FirstOrDefault();
            return reservation;
        }
        public List<Reservation> GetReservationByUserId(int userId)
        {
            var reservation = _connection.Reservations.Include(r=>r.CompanyMainPhoto).Where(r=>r.UserId == userId).ToList();
            return reservation;
        }
        public List<Reservation> GetAll()
        {
            var reservations = _connection.Reservations.ToList();
            return reservations;
        }
    }
}
