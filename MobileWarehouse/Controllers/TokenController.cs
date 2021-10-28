using Microsoft.AspNetCore.Mvc;
using MobileWarehouse.Common.Models;
using MobileWarehouse.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace MobileWarehouse.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            var response = await _tokenRepository.GetToken(model);
            if (response is string)
            {
                return BadRequest(new { errorText = response });
            }
            if (true)
            {
                return Json(response);
            }
        }
    }
}
