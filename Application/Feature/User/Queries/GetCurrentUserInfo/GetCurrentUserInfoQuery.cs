using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Data.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Feature.User.Queries.GetCurrentUserInfo
{
    public class GetCurrentUserInfoQuery : IRequest<CurrentUserDto>
    {
    }
    
    public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, CurrentUserDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserInfoQueryHandler(ApplicationDbContext context, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<CurrentUserDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(usr => usr.Id == _currentUserService.UserId.ToString())
                .ProjectTo<CurrentUserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}