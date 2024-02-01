using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface IPhotoDataAccess
    {
        public Photo GetPhotoById(int id);
        public Photo GetPhotoByCityId(int id);
        public List<Photo> GetPhotoList();
        public int AddPhoto(Photo photo);
        CompanyPhoto GetCompanyPhotoByCityId(int companyId);
        int AddCompanyPhoto(CompanyPhoto photo);
    }
}
