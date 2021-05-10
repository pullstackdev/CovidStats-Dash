using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Models
{
    //şehirleri enum ile tutalım
    public enum ECity
    {
        Istanbul = 1,
        Ankara = 2,
        Izmir = 3,
        Konya = 4,
        Antalya = 5
    }
    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; } // db enumdaki rakamları (int) tutacak
        public int CaseCount { get; set; } //vaka sayısı
        public DateTime CovidDate { get; set; }
    }
}
