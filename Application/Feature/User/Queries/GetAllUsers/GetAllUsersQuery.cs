using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UsersDto>>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery,IEnumerable<UsersDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .ProjectTo<UsersDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}