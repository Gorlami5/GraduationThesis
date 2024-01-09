using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit
{
    public class AuthDataAccess : IAuthDataAccess
    {
        private readonly PostgreDbConnection _connection;
        public AuthDataAccess(PostgreDbConnection connection)
        {
           _connection = connection;
        }
        public int AddUser(User user)
        {
            _connection.Add(user);
            return _connection.SaveChanges();
        }

        public User GetuserByEmail(string email)
        {
            var user = _connection.Users.FirstOrDefault(e=> e.Email == email); //Dbye email eklemeyi unutmuşuz.Ekle!!!
            return user;
        }
        public int SessionCreate(Session session)
        {
            _connection.Add(session);
            return _connection.SaveChanges();
        }
        public Session GetActiveSession(string token) // Use to list
        {
            var session = _connection.Sessions.Where(s=>s.AccessToken == token && s.IsActive == true).FirstOrDefault();
            return session;
        }
        public Session GetActiveSessionByUserId(int id) 
        {
            var session = _connection.Sessions.Where(s => s.UserId == id && s.IsActive == true).FirstOrDefault();
            return session;
        }
        public int SessionUpdate(Session session)
        {
            _connection.Update(session);
            return _connection.SaveChanges();
        }



    }
}
