using FluentValidation;
using ReservationApp.Dto;

namespace ReservationApp.Utilities.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("empty olamaz!");
            RuleFor(x => x.UserSurname).NotNull();

        }
    }
}
