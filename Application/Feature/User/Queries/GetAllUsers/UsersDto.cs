using Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace Application.Feature.User.Queries.GetAllUsers
{
    public class UsersDto : IMapFrom<IdentityUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}