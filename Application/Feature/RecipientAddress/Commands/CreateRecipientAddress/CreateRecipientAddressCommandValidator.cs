using System.Threading;
using System.Threading.Tasks;
using Application.Data.Persistence;
using Application.Events;
using FluentValidation;
using MediatR;

namespace Application.Feature.RecipientAddress.Commands.CreateRecipientAddress
{
    public class CreateRecipientAddressCommandValidator : AbstractValidator<CreateRecipientAddressCommand>
    {
        public CreateRecipientAddressCommandValidator()
        {
            // RuleFor(v => v.Address)
            //     .MaximumLength(200)
            //     .NotEmpty();
        }
    }
}