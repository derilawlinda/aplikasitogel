﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.DAL.Models
{
    public class WinningDetail
    {
        public int BetNumber { get; set; }
        public double Winning { get; set; }
        
        public double BetAmount { get; set; }
        public double Discount { get; set; }
    }
}