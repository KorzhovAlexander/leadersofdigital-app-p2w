using System.Threading.Tasks;
using Application.Feature.User.Queries.GetAllUsers;
using Application.Feature.User.Queries.GetCurrentUserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    // [Authorize]
    public class UserController : ApiController
    {
        //GET: api/User/current-user
        /// <summary>
        /// Получить текущего пользователя и его роли
        /// </summary>
        [HttpGet("current-user")]
        public async Task<ActionResult<CurrentUserDto>> Get()
        {
            return Ok(await Mediator.Send(new GetCurrentUserInfoQuery()));
        }
        
        [HttpGet("all-users")]
        public async Task<ActionResult<CurrentUserDto>> GetAllUsers()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }
    }
}