using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.UsersAddresses.Queries
{
    public class GetUsersAddresses : IRequest<IQueryable<UsersAddressesDto>>
    {
    }
    
    public class GetUsersAddressesHandler : IRequestHandler<GetUsersAddresses,IQueryable<UsersAddressesDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersAddressesHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IQueryable<UsersAddressesDto>> Handle(GetUsersAddresses request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.UsersAddresses
                .ProjectTo<UsersAddressesDto>(_mapper.ConfigurationProvider)
                .AsNoTracking());
        }
    }
}