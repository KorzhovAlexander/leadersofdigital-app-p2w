using Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace Application.Feature.User.Queries.GetCurrentUserInfo
{
    public class CurrentUserDto : IMapFrom<IdentityUser>
    {
        public string UserName { get; set; }
        public string Id { get; set; }
    }
}