using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface IReservationDataAccess
    {
        int Add(Reservation reservation);
        int Delete(Reservation reservation);
        int Update(Reservation reservationCompany);
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationByUserId(int userId);
        List<Reservation> GetAll();
    }
}
