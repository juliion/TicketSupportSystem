using FluentValidation;
using TicketSupportSystem.DTOs.Requests;

namespace TicketSupportSystem.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator() 
        {
            RuleFor(uc => uc.Text)
                 .NotNull()
                 .NotEmpty();
        }
    }
}
