using RealEyesHomework.Dtos;
using RealEyesHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEyesHomework.Services
{
    public interface IExchangesService
    {
        Task<IReadOnlyList<CurrencyRatesDto>> Latest();
        Task<IReadOnlyList<ExchangeRate>> GetByName(string name);
    }
}
