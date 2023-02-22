using FluentValidation;
using Wholesaler.Data;

namespace Wholesaler.DataTransferObject.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public RegisterUserDtoValidator(DataContext contextDb)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = contextDb.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });      
        }
    }
}
