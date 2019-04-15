using GenericCodes.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.DAL.Models
{
    public class Transaction : Entity
    {
        public Transaction()
        {
        }

        [Key]
        public int TransactionID { get; set; }

        public int UserID { get; set; }
        public double BetAmount { get; set; }
        public int BetNumber { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }

    }
}
