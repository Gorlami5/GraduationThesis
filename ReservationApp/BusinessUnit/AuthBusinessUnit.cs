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
            int userId2 = userToLogin.Data.Id;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
               {
                    new Claim (ClaimTypes.NameIdentifier, userToLogin.Data.Id.ToString()),
                    new Claim (ClaimTypes.Email,userToLogin.Data.Email)
               }),
                Expires = DateTime.Now.AddDays(10000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            //-------------------------------------------------Transaction Yapısı--------------------------------------------

            using(var transaction = _connection.Database.BeginTransaction())
            {
                try
                {
                    DateTime myDateTime = DateTime.Now;
                    string formattedDateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    var emptySession = new Session()
                    {
                        CreateDate = formattedDateTime,
                    };
                    var emptySessionResult = _authDataAccess.SessionCreate(emptySession);
                    if (emptySessionResult < 1)
                    {
                        return new ErrorDataResult<string>(ConstantsMessages.EmptySessionNotCreated);
                    }
                  
                    var oldSession = _authDataAccess.GetActiveSessionByUserId(userId2);
                    if (oldSession != null)
                    {
                        oldSession.IsActive = false;
                        var sessionUpdateResult = _authDataAccess.SessionUpdate(oldSession);
                        if (sessionUpdateResult < 1)
                        {
                            return new ErrorDataResult<string>(ConstantsMessages.OldSessionNotCreated);
                        }
                    }
                    emptySession.AccessToken = tokenString;
                    emptySession.UserId = userId2;
                    emptySession.IsActive = true;


                    var sessionResult = _authDataAccess.SessionUpdate(emptySession);

                    //var session = new Session()
                    //{
                    //    AccessToken = tokenString,
                    //    CreateDate = DateTime.Now,
                    //    IsActive = true,
                    //    UserId = userToLogin.Data.Id
                    //};

                    //var sessionResult = _authDataAccess.SessionCreate(session);
                    if (sessionResult < 1)
                    {
                        return new ErrorDataResult<string>(ConstantsMessages.SessionNotCompleted);
                    }
                    transaction.Commit();
                    return new SuccessDataResult<string>(tokenString, ConstantsMessages.LoginSuccess);

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return new ErrorDataResult<string>(ex.Message);
                }
            }

            //DateTime myDateTime = DateTime.Now;
            //string formattedDateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            //var emptySession = new Session()
            //{
            //    CreateDate = formattedDateTime,
            //};
            //var emptySessionResult = _authDataAccess.SessionCreate(emptySession);
            //if (emptySessionResult < 1)
            //{
            //    return new ErrorDataResult<string>(ConstantsMessages.EmptySessionNotCreated);
            //}
            //var oldSession = _authDataAccess.GetActiveSessionByUserId(userId2);
            //if (oldSession != null)
            //{
            //    oldSession.IsActive = false;
            //    var sessionUpdateResult = _authDataAccess.SessionUpdate(oldSession);
            //    if (sessionUpdateResult < 1)
            //    {
            //        return new ErrorDataResult<string>(ConstantsMessages.OldSessionNotCreated);
            //    }
            //}
            //emptySession.AccessToken = tokenString;
            //emptySession.UserId = userId2;
            //emptySession.IsActive = true;


            //var sessionResult = _authDataAccess.SessionUpdate(emptySession);

            //if (sessionResult < 1)
            //{
            //    return new ErrorDataResult<string>(ConstantsMessages.SessionNotCompleted);
            //}

            //return new SuccessDataResult<string>(tokenString, ConstantsMessages.LoginSuccess);


            //Login olunduğu durumda bir session oluşturulacak.Session tablosuna yeni bir session eklenecek.Active true olacak.
            //Tabi bu sessionu sonlandırmak için bir logout yazmamız gerekecek.
            //Ama birkez daha giriş yapmak isterse aynı kullanıcı daha önceki sessionların activlerini kontrol eder ve yoksa bir şey yapmaz varsa kapatır.
            //Expire süresi sonsuz olacak şekilde ayarlanacak.
            //Anlık kullanıcının sessionla beraber tüm bilgilerini dönderen bir servis yazılacak.
           
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
        public DataResult<Session> GetActiveSessionByToken(string token)
        {
            var session = _authDataAccess.GetActiveSession(token);
            if(session == null)
            {
                return new ErrorDataResult<Session>(ConstantsMessages.SessionNotFound);
            }
            return new SuccessDataResult<Session>(session,ConstantsMessages.SessionListed);
        }
        public Result Logout(string token)
        {
            var session = _authDataAccess.GetActiveSession(token);
            if (session == null)
            {
                return new ErrorResult(ConstantsMessages.SessionNotFound);
            }
            session.IsActive = false;
            var logoutResult = _authDataAccess.SessionUpdate(session);
            if(logoutResult > 0)
            {
                return new SuccessResult(ConstantsMessages.LogoutSuccess);              
            }
            return new ErrorResult(ConstantsMessages.LogoutFailed);

        }
    }
}
