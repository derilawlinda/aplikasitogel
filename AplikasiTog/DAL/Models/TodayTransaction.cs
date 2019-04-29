using GenericCodes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.DAL.Models
{
    public class TodayTransaction
    {
        public TodayTransaction()
        {
        }
        public string UserName { get; set; }
        public double BetAmount { get; set; }
        public int BetNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
