using GenericCodes.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.DAL.Models
{
    public class Nomor : Entity
    {
        public Nomor()
        {
        }
        public int NomorID { get; set; }

        public int WinningNomor { get; set; }
        public DateTime Date { get; set; }
                          
    }
}
