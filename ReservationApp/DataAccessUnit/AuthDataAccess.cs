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
    }
}
