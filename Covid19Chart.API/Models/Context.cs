using Microsoft.EntityFrameworkCore; //for dbcontext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) //farklı bir connstr yöntemi
        {
            //appsettings.json'a tanımlanır
            //configurationService de services.AddDbContext eklendi ayarlama yapıldı
        }

        public DbSet<Covid> Covids { get; set; } //çoğul ve büyük harf tablo ismi standartı
    }
}
