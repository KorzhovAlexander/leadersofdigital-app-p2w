

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.RecipientAddress.Queries.GetRecipientAddress
{
    public class GetRecipientAddressQuery : IRequest<IEnumerable<RecipientAddressDto>>
    {
    }

    public class RecipientAddressQueryHandler : IRequestHandler<GetRecipientAddressQuery, IEnumerable<RecipientAddressDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RecipientAddressQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipientAddressDto>> Handle(GetRecipientAddressQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.RecipientAddresses
                .ProjectTo<RecipientAddressDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}