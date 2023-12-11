using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.BusinessUnit.Interfaces
{
    public interface IPhotoBusinessUnit
    {
        public DataResult<Photo> GetPhotoByCityId(int cityId);
        public DataResult<List<Photo>> GetPhotoList();
        public DataResult<PhotoForReturnDto> GetPhotoById(int id);
        public DataResult<PhotoForReturnDto> AddPhotoForCity(int cityId, PhotoForCreationDto photoForCreationDto);
    }
}
