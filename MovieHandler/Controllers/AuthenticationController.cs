using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieHandler.Abstraction.Business;

namespace MovieHandler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _favoritesManager;
        public AuthenticationController(IAuthenticationManager favoritesManager)
        {
            _favoritesManager = favoritesManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Favorite)
        {

        }
    }
}
