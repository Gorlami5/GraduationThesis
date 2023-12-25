using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservationApp.BusinessUnit
{
    public class AuthBusinessUnit : IAuthBusinessUnit
    {
        private readonly PostgreDbConnection _connection;
        private readonly IAuthDataAccess _authDataAccess;
        private readonly IConfiguration _configuration;
        public AuthBusinessUnit(PostgreDbConnection connection,IAuthDataAccess authDataAccess, IConfiguration configuration)
        {
            _connection = connection;
            _authDataAccess = authDataAccess;
            _configuration = configuration;
        }
        public DataResult<User> UserForLogin(string email, string password)
        {
            var user  = _authDataAccess.GetuserByEmail(email);

            if (user == null)
            {
               return new ErrorDataResult<User>(ConstantsMessages.UserNotFound);
            }

            if (!HashExtension.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                return new ErrorDataResult<User>(ConstantsMessages.WrongPassword);
            }
          return new SuccessDataResult<User>(user);
        }
        public DataResult<string> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = UserForLogin(userForLoginDto.Email, userForLoginDto.Password);
            if (userToLogin == null)
            {
                return new ErrorDataResult<string>(ConstantsMessages.LoginError);
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
               {
                    new Claim (ClaimTypes.NameIdentifier, userToLogin.Data.Id.ToString()),
                    new Claim (ClaimTypes.Email,userToLogin.Data.Email)
               }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new SuccessDataResult<string>(tokenString,ConstantsMessages.LoginSuccess);
        }
        public DataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = UserExist(userForRegisterDto.Email);
            if(userExist == true)
            {
                return new ErrorDataResult<User>(ConstantsMessages.AlreadyUserExist);
            }
            var createToUser = new User()
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.UserName,
                UserSurname = userForRegisterDto.UserSurname
            };
            var createdUser = UserForRegister(createToUser, userForRegisterDto.Password);
            if(createdUser.Data == null)
            {
                return new ErrorDataResult<User>(ConstantsMessages.RegisterFail);
            }
            return new SuccessDataResult<User>(createdUser.Data, ConstantsMessages.RegisterSuccess);

        }

        public DataResult<User> UserForRegister(User user, string password)
        {
            byte[] passwordhash, passwordsalt;
            HashExtension.CreatePasswordHash(password, out passwordhash, out passwordsalt);
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordsalt;
            var result = _authDataAccess.AddUser(user);
            if(result > 0)
            {
                return new SuccessDataResult<User>(user,ConstantsMessages.LoginError);
            }
            
            return new ErrorDataResult<User>();
        }

        public bool UserExist(string email)
        {
            var user = _authDataAccess.GetuserByEmail(email);
            if(user == null)
            {
                return false;
            }
            return true;
        }
    }
}
