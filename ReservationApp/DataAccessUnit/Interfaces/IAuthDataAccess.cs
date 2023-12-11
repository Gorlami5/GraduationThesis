using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface IAuthDataAccess
    {
        public int AddUser(User user);
        public User GetuserByEmail(string email);
    }
}
