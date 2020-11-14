using System;
using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
        // public string Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        public string UserId => "1b8fe6b9-0cb1-4b3c-98b8-bc3cb03db46f";

        public string Username => "Коржов А.А.";
    }
}
