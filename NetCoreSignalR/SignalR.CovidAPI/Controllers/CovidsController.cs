using Microsoft.AspNetCore.Mvc;
using SignalR.CovidAPI.Models;
using SignalR.CovidAPI.Models.Enums;
using SignalR.CovidAPI.Models.ViewModels;
using SignalR.CovidAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.CovidAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {
        private readonly ICovidService _covidService;

        public CovidsController(ICovidService covidService)
        {
            _covidService = covidService;
        }

        /// <summary>
        /// Veri tabaına covid vakasının eklenmesi ve covid listesinin dönülmesi
        /// </summary>
        /// <param name="covid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            var covidsChartViewModel = await _covidService.AddCovidAsync(covid);

            return Ok(covidsChartViewModel);
        }

        /// <summary>
        /// Rastgele şehirlere covid vakası ekler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InitilazeCovid()
        {
            Random random = new Random();

            Enumerable.Range(1, 10).ToList().ForEach(randomValue =>
            {
                foreach (City city in Enum.GetValues(typeof(City)))
                {
                    var covid = new Covid
                    {
                        City = city,
                        NumberOfCases = random.Next(100, 1000),
                        CovidDate = DateTime.Now.AddDays(randomValue)
                    };

                    _covidService.AddCovidAsync(covid).Wait();

                    Thread.Sleep(TimeSpan.FromSeconds(2)); //2 saniye arayla verileri ekle.
                }
            });

            return Ok();
        }
    }
}
