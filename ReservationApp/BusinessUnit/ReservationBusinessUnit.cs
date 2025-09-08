using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;
using System.Security.Claims;

namespace ReservationApp.BusinessUnit
{
    public class ReservationBusinessUnit:IReservationBusinessUnit
    {
        private readonly IReservationDataAccess _reservationDataAccess;
        private readonly IHttpContextAccessor _contextAccessor;
        public ReservationBusinessUnit(IReservationDataAccess reservationDataAccess, IHttpContextAccessor contextAccessor)
        {
            _reservationDataAccess = reservationDataAccess;
            _contextAccessor = contextAccessor;
        }

        public Result AddReservation(Reservation reservation)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId != null)
            {
                reservation.UserId = int.Parse(userId);
            }          
            var reservations = _reservationDataAccess.GetAll();
            foreach(var res  in reservations)
            {
               //companyId ve resStart ve resEnd kontrolü yapılacak.
               //Eğer aynı durumda bir rezervasyon varsa Ekleme yapamayacak.
              
               if(res.CompanyId == reservation.CompanyId)
                {
                    if(reservation.ReservationStartDate >= res.ReservationStartDate && reservation.ReservationEndDate <= res.ReservationEndDate)
                    {
                        return new ErrorResult(ConstantsMessages.ReservationDateTimeError);
                    }

                }
            }
            var company = _reservationDataAccess.GetCompanyById(reservation.CompanyId);
            var reservationInterval = reservation.ReservationStartDate - reservation.ReservationEndDate;
            if(reservationInterval.Days > company.MaxReservationDayLength)
            {
                return new ErrorResult(ConstantsMessages.MaxReservationDayError); 
            }
            var result = _reservationDataAccess.Add(reservation);
            if(result >  0)
            {
                return new SuccessResult(ConstantsMessages.ReservationAdded);
            }
            return new ErrorResult(ConstantsMessages.ReservationAddError);
        }

        public DataResult<Reservation> GetReservationById(int id)
        {
            var reservation = _reservationDataAccess.GetReservationById(id);
            if(reservation != null)
            {
               return new SuccessDataResult<Reservation>(reservation,ConstantsMessages.ReservationListed);
            }
            return new ErrorDataResult<Reservation>(ConstantsMessages.ReservationListeError);
        }

        public DataResult<List<ReservationForListDto>> GetReservationByUserId(int userId)
        {
            var reservations = _reservationDataAccess.GetReservationByUserId(userId);
            List<ReservationForListDto> reservationList = new List<ReservationForListDto>();
            if(reservations != null)
            {
                foreach(var reservation in reservations)
                {
                    var company = _reservationDataAccess.GetCompanyById(reservation.CompanyId);

                       ReservationForListDto reservationDto = new ReservationForListDto()
                       {
                           CityName = reservation.CityName,
                           CompanyName = reservation.CompanyName,
                           ReservationStartDate = reservation.ReservationStartDate,
                           ReservationEndDate = reservation.ReservationEndDate,
                           PersonCount = reservation.PersonCount
                       };
                    reservationList.Add(reservationDto);
                }
                if(reservationList != null)
                {
                    return new SuccessDataResult<List<ReservationForListDto>>(reservationList,ConstantsMessages.ReservationListed);
                }

            }
            return new ErrorDataResult<List<ReservationForListDto>>(ConstantsMessages.ReservationListeError);
        }

        public Result RemoveReservation(Reservation reservation)
        {
            var result =  _reservationDataAccess.Delete(reservation);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.ReservationDeleted);
            }
            return new ErrorResult(ConstantsMessages.ReservationDeleteError);
        }

        public Result UpdateReservation(Reservation updatedReservation)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                updatedReservation.UserId = int.Parse(userId);
            }
            var result = _reservationDataAccess.Update(updatedReservation);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.ReservationUpdated);
            }
            return new ErrorResult(ConstantsMessages.ReservationUpdateError);
        }
    }
}
