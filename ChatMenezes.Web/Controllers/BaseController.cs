using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatMenezes.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public string UsuarioLogadoId
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
