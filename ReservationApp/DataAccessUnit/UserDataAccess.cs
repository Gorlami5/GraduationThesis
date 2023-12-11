using ReservationApp.Context;

namespace ReservationApp.DataAccessUnit
{
    public class UserDataAccess
    {
        PostgreDbConnection _connection;
        public UserDataAccess(PostgreDbConnection connection) 
        {
            _connection = connection;
        }


    }
}
