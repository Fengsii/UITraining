using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UITraining.Interfaces;

namespace UITraining.Services
{
    public class LoginLayoutService : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginLayoutService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Method untuk mendapatkan layout berdasarkan peran pengguna
        public string GetLayout()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.IsInRole("Admin"))
            {
                return "~/Views/Shared/_Layout.cshtml"; // Layout untuk Admin
            }
            else if (user.IsInRole("User"))
            {
                return "~/Views/Shared/_LayoutUser.cshtml"; // Layout untuk User
            }

            return "~/Views/Shared/_Layout.cshtml"; // Default layout jika tidak ada peran
        }

    }
}
