using Covid19Chart.API.Hubs; //for CovidHub
using Covid19Chart.API.Models; //for Context classd
using Microsoft.AspNetCore.SignalR; //for IHubContext
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Services
{
    public class CovidService //db ile ilgili işlemler/metodlar burada olacak
    {
        private readonly Context _context;
        private readonly IHubContext<CovidHub> _hubContext; //Hub'a Hub classı dışından erişmek için kullanılır

        public CovidService(Context context, IHubContext<CovidHub> hubContext) //dependency injection
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList() //tüm data getir
        {
            return _context.Covids.AsQueryable(); //sadece sorguyu datadan çeker, inumerable (tolist) ise tüm datayı çeker önce
        }
        public async Task SaveCovid(Covid covid) //yeni covid a kaydet
        {
            await _context.Covids.AddAsync(covid);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidChartList()); //yeni covid data eklenince tüm clientlara ilet/duyur
        }
        public List<CovidChartPivot> GetCovidChartList()
        {
            List<CovidChartPivot> covidChartPivots = new List<CovidChartPivot>();

            //CovidChartPivot entity değil complext type ve id si olmadığı için ef kulanılamayacak, yerine sorgu yazılacak
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"SELECT CovidDate, [1],[2],[3],[4],[5] FROM
                                (select[City], [CaseCount], Cast([CovidDate] as date) as CovidDate from Covids) as CovidTable

                                PIVOT (SUM([CaseCount]) FOR City IN( [1], [2], [3], [4], [5])) AS PivotTable
                                order by CovidDate asc";

                command.CommandType = System.Data.CommandType.Text; //sorgu tipi beli edildi.

                _context.Database.OpenConnection(); //bağlantı açılşdı
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidChartPivot ccp = new CovidChartPivot();
                        ccp.CovidDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                ccp.Counts.Add(0);
                            }
                            else
                            {
                                ccp.Counts.Add(reader.GetInt32(x));
                            }
                        });

                        covidChartPivots.Add(ccp);
                    }
                }

                _context.Database.CloseConnection();

                return covidChartPivots;
            }

        }
    }
}
