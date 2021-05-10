using Covid19Chart.API.Services;
using Microsoft.AspNetCore.SignalR; //for :Hub
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Hubs
{
    public class CovidHub :Hub
    {
        private readonly CovidService _covidService;
        public CovidHub(CovidService covidService)
        {
            _covidService = covidService;
        }
        public async Task GetCovidList() //clienttan bu metod çağırılınca (invoke edilecek) hepsine db den veriler iletilecek
        {
            await Clients.All.SendAsync("ReceiveCovidList", _covidService.GetCovidChartList()); //client ReceiveCovidList on ile subscribe olmalı ki mesaj onada gelsin
        }
    }
}
