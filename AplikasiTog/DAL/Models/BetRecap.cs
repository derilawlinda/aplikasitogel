using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.DAL.Models
{
    public class BetRecap
    {
        public BetRecap()
        {
        }
        public int BetNumber { get; set; }
        public double TotalBetAmount { get; set; }

        public List<BetRecapDetail> BetRecapDetails {get; set;}
    }
}
