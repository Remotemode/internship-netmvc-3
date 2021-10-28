using Microsoft.AspNetCore.Mvc;
using MobileWarehouse.Repository.Interface;
using System;

namespace MobileWarehouse.Controllers
{
    public class DiscountController : Controller
    {
        private readonly ITokenRepository _tokenRepository;

        public DiscountController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        [HttpGet("/discount")]
        public IActionResult Index([FromHeader(Name = "Authorization")] string token)
        {
            if (_tokenRepository.ValidateToken(token))
            {
                var random = new Random();

                return Ok($"Discount for your product {random.Next(1, 50)}%");
            }
            else
            {
                return Unauthorized(new { errorText = "Invalid token" });
            }
        }
    }
}
