using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.DAL.Models
{
    public class Winning
    {
        public string UserName { get; set; }
        public double TotalWinning { get; set; }

        public List<WinningDetail> WinningDetails { get; set; }

        
    }
}
