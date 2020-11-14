using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Data.Persistence;
using MediatR;

namespace Application.Feature.RecipientAddress.Commands.DeleteRecipientAddress
{
    public class DeleteRecipientAddressCommand: IRequest
    {
        public int Id { get; set; }
    }
    
    public class DeleteRecipientAddressCommandHandler : IRequestHandler<DeleteRecipientAddressCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteRecipientAddressCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteRecipientAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.RecipientAddresses.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(RecipientAddress), request.Id);
            }

            _context.RecipientAddresses.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}