using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.BusinessUnit.Interfaces
{
    public interface IAuthBusinessUnit
    {
        public DataResult<User> UserForRegister(User user, string password);

        public DataResult<User> Register(UserForRegisterDto userForRegisterDto);

        public DataResult<User> UserForLogin(string email, string password);

        public bool UserExist(string email);

        public DataResult<string> Login(UserForLoginDto userForLoginDto);
        DataResult<Session> GetActiveSessionByToken(string token);
    }
}
