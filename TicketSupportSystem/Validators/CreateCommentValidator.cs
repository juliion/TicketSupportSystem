using FluentValidation;
using TicketSupportSystem.DTOs.Requests;

namespace TicketSupportSystem.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentValidator()
        {
            RuleFor(cc => cc.Text)
                .NotNull()
                .NotEmpty();
            RuleFor(cc => cc.UserId)
                .NotNull();
            RuleFor(cc => cc.TicketId)
                .NotNull();
        }
    }
}
