using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface IAuthDataAccess
    {
        public int AddUser(User user);
        public User GetuserByEmail(string email);
        int SessionCreate(Session session);
        Session GetActiveSession(string token);
        Session GetActiveSessionByUserId(int id);
        int SessionUpdate(Session session);
    }
}
