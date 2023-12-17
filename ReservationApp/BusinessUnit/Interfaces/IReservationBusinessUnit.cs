using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.BusinessUnit.Interfaces
{
    public interface IReservationBusinessUnit
    {
        Result AddReservation(Reservation reservation);
        Result RemoveReservation(Reservation reservation);
        Result UpdateReservation(Reservation updatedReservation);
        DataResult<Reservation> GetReservationById(int id);
        DataResult<List<ReservationForListDto>> GetReservationByUserId(int userId);
    }
}
