using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEyesHomework.Services;
using RealEyesHomework.Models;
using RealEyesHomework.Dtos;

namespace RealEyesHomework.Controllers
{
    [Route("api/[controller]")]
    public class ExchangesController : Controller
    {
        public ExchangesController(
            IExchangesService service
        )
        {
            _service = service;
        }

        private readonly IExchangesService _service;

        // GET api/values
        [HttpGet("Latest")]
        public async Task<IEnumerable<CurrencyRatesDto>> GetLatest()
        {
            return await _service.Latest();
        }

        // GET api/values
        [HttpGet("Currency/{name}")]
        public async Task<IEnumerable<ExchangeRate>> GetByName(string name)
        {
            return await _service.GetByName(name);
        }
    }
}
