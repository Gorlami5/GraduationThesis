using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.BusinessUnit
{
    public class ReservationBusinessUnit:IReservationBusinessUnit
    {
        private readonly IReservationDataAccess _reservationDataAccess;
        public ReservationBusinessUnit(IReservationDataAccess reservationDataAccess)
        {
            _reservationDataAccess = reservationDataAccess;
        }

        public Result AddReservation(Reservation reservation)
        {
            var reservations = _reservationDataAccess.GetAll();
            foreach(var res  in reservations)
            {
               //companyId ve resStart ve resEnd kontrolü yapılacak.
               //Eğer aynı durumda bir rezervasyon varsa Ekleme yapamayacak.
               //Company tarafına DateTime tipinde kolon eklenecek ve rez yapılmayacak tarihleri tutacak!!!
               //Yine company tarafına MaxRezDateLength eklenecek fazlası olursa rez yapılmayacak.
               if(res.CompanyId == reservation.CompanyId)
                {
                    if(reservation.ReservationStartDate >= res.ReservationStartDate && reservation.ReservationEndDate <= res.ReservationEndDate)
                    {
                        return new ErrorResult(ConstantsMessages.ReservationDateTimeError);
                    }

                }
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
                       ReservationForListDto reservationDto = new ReservationForListDto()
                       {
                           CityName = reservation.CityName,
                           CompanyName = reservation.CompanyName,
                           ReservationStartDate = reservation.ReservationStartDate,
                           ReservationEndDate = reservation.ReservationEndDate,
                           CompanyMainPhoto = reservation.CompanyMainPhoto, //Logic hatası var bu photo durumunda
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
            var result = _reservationDataAccess.Update(updatedReservation);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.ReservationUpdated);
            }
            return new ErrorResult(ConstantsMessages.ReservationUpdateError);
        }
    }
}
