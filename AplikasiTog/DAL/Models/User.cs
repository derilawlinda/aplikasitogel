using GenericCodes.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AplikasiTog.DAL.Models
{
    public class User : Entity
    {
        public User()
        {
        }

        [Key]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Discount { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
