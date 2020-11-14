using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Data.Persistence;
using MediatR;

namespace Application.Feature.RecipientAddress.Commands.UpdateRecipientAddress
{
    public class UpdateRecipientAddressCommand: IRequest
    {
        public int Id { get; set; }
        
        public string Address { get; set; }
    }
    
    public class UpdateRecipientAddressCommandHandler : IRequestHandler<UpdateRecipientAddressCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateRecipientAddressCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateRecipientAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.RecipientAddresses.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(RecipientAddress), request.Id);
            }

            entity.Address = request.Address;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;        
        }
    }
}