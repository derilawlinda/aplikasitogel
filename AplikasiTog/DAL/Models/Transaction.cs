using GenericCodes.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Range(10, int.MaxValue, ErrorMessage = "Nomor harus lebih dari 2 digit")]
        public int BetNumber { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }



    }
}
