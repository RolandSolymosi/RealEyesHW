using RealEyesHomework.Dtos;
using RealEyesHomework.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEyesHomework.Services
{
    public class ExchangesService : IExchangesService
    {
        private IReadOnlyList<Currency> Currencies;
        private DateTimeOffset LastRefreshDateTimeOffset;

        private async Task Refresh()
        {
            // Cache timer checking
            if (LastRefreshDateTimeOffset == null || (DateTimeOffset.Now - LastRefreshDateTimeOffset).TotalMinutes > 1)
            {
                // Set new timestamp
                LastRefreshDateTimeOffset = DateTimeOffset.Now;

                using (var client = new HttpClient())
                using (var response = await client.GetAsync(new Uri("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml", UriKind.Absolute)))
                {
                    response.EnsureSuccessStatusCode();

                    try
                    {
                        var responseXML = XDocument.Parse(await response.Content.ReadAsStringAsync());

                        Currencies = responseXML.Root.DescendantNodes().OfType<XElement>()
                            .Where(n => n.Name.LocalName == "Cube" && n.Attributes().Any(a => a.Name == "currency"))
                            .GroupBy(n => n.Attribute("currency").Value)
                            .Select(g => new Currency
                            {
                                Name = g.Key,
                                ExchangeRates = g.Select(e => new ExchangeRate
                                {
                                    Date = DateTimeOffset.Parse(e.Parent.Attribute("time").Value),
                                    Rate = decimal.Parse(e.Attribute("rate").Value, NumberStyles.Any, CultureInfo.InvariantCulture)
                                }).ToList()
                            }).ToList();
                    }
                    catch (Exception ex)
                    {
                        throw new ExchangeServiceException("Issue with the XML parsing!", ex);
                    }
                }
            }
        }

        public async Task<IReadOnlyList<CurrencyRatesDto>> Latest()
        {
            await Refresh();
            return Currencies.Select(c => new CurrencyRatesDto
            {
                Name = c.Name,
                Rate = c.ExchangeRates.OrderByDescending(er => er.Date).First().Rate
            }).ToList();
        }

        public async Task<IReadOnlyList<ExchangeRate>> GetByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            await Refresh();
            return Currencies.Single(c => c.Name == name).ExchangeRates.ToList();
        }
    }

    public class ExchangeServiceException : Exception
    {
        public ExchangeServiceException() { }
        public ExchangeServiceException(string message) : base(message) { }
        public ExchangeServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
