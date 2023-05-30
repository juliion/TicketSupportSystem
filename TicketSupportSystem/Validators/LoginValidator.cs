using FluentValidation;
using System.Text.RegularExpressions;
using TicketSupportSystem.DTOs.Requests;

namespace TicketSupportSystem.Validators
{
    public class LoginValidator : AbstractValidator<UserLoginDTO>
    {
        public LoginValidator() 
        {
            RuleFor(ul => ul.Email)
                 .NotNull()
                 .EmailAddress();
            RuleFor(ut => ut.Password)
                 .NotNull()
                 .NotEmpty()
                 .Length(5, 15);
        }
    }
}
