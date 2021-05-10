using Covid19Chart.API.Models;
using Covid19Chart.API.Services; //for CovidService
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {
        private readonly CovidService _covidService;
        public CovidsController(CovidService covidService)
        {
            _covidService = covidService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            await _covidService.SaveCovid(covid);

            //IQueryable<Covid> covidList = _service.GetList();
            return Ok(_covidService.GetCovidChartList());
        }

        public IActionResult InitializeData() //random covid datası oluşturma, simulasyon
        {
            Random rnd = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x => //10 gün için
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity))) //enumdaki her değer için
                {
                    var newCovid = new Covid { City = item, CaseCount = rnd.Next(100, 1000), CovidDate = DateTime.Now.AddDays(x) };
                    _covidService.SaveCovid(newCovid).Wait(); //awaiti direk kanul etmedi, aynı işlemi yapan waiti kabul etti
                    System.Threading.Thread.Sleep(1000); //1sn bekle sonra data ekle
                }
            });

            return Ok("Covid19 dataları db ye kaydedildi");
        }
    }
}
