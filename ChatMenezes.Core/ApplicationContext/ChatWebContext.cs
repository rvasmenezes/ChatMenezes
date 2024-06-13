using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatMenezes.Core.ApplicationContext
{
    public class ChatWebContext : IChatWebContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatWebContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserLoggedId
        {
            get
            {
                var user = _httpContextAccessor?.HttpContext?.User;
                var nameIdentifier = user?.FindFirstValue(ClaimTypes.NameIdentifier);
                return nameIdentifier!;
            }
        }

        public string UserLoggedEmail
        {
            get
            {
                var user = _httpContextAccessor?.HttpContext?.User;
                var emailUsuarioLogado = user?.FindFirstValue(ClaimTypes.Email);
                return emailUsuarioLogado!;
            }
        }

        public string? UserLoggedName
            => _httpContextAccessor?.HttpContext?.Session.GetString("UserName");
    }
}
