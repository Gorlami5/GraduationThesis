using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit
{
    public class PhotoDataAccess:IPhotoDataAccess
    {
        private readonly PostgreDbConnection _connection;
        public PhotoDataAccess(PostgreDbConnection connection)
        {
                _connection = connection;
        }

        public Photo GetPhotoById(int id)
        {
            var photo = _connection.Photos.Where(c  => c.Id == id).FirstOrDefault();
            return photo;
        }
        public Photo GetPhotoByCityId(int id)
        {
            var photo = _connection.Photos.Where(c => c.CityId == id).FirstOrDefault();
            return photo;
        }
        public CompanyPhoto GetCompanyPhotoByCityId(int companyId)
        {
            var photo = _connection.CompanyPhotos.Where(c => c.CompanyId == companyId).FirstOrDefault();
            return photo;
        }


        public List<Photo> GetPhotoList()
        {
            var photos = _connection.Photos.ToList();
            return photos;
        }
        public int AddPhoto(Photo photo)
        {
            _connection.Add(photo);
            return _connection.SaveChanges();
        }
    }
}
