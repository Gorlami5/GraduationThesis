using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.BusinessUnit;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationBusinessUnit _reservationBusinessUnit;
        public ReservationController(IReservationBusinessUnit reservationBusinessUnit)
        {
            _reservationBusinessUnit = reservationBusinessUnit;
        }
        [Authorize]
        [HttpPost]
        [Route("AddReservation")]
        public Result AddReservation([FromBody] Reservation reservation)
        {
            var result = _reservationBusinessUnit.AddReservation(reservation);
            return result;
        }
        [Authorize]
        [HttpPost]
        [Route("UpdateReservation")]
        public Result UpdateReservation([FromBody] Reservation reservation)
        {
            return _reservationBusinessUnit.UpdateReservation(reservation);
        }
        [HttpPost]
        [Route("RemoveReservation")]
        public Result DeleteReservation([FromBody] Reservation reservation)
        {
            return _reservationBusinessUnit.RemoveReservation(reservation);
        }
        [HttpGet]
        [Route("GetReservationListByUserId")]
        public DataResult<List<ReservationForListDto>> GetReservationListByUserId(int userId)
        {
            return _reservationBusinessUnit.GetReservationByUserId(userId);
        }
        [HttpGet]
        [Route("GetReservationByUserId")]
        public DataResult<Reservation> GetReservationById(int id)
        {
            return _reservationBusinessUnit.GetReservationById(id);
        }
    } 

    }


   

