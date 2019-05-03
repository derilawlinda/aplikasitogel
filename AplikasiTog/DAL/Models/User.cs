using GenericCodes.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Apel.DAL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Apel.DAL.Models
{
    public class User : Entity
    {
        public User()
        {
            
        }

        [Key]
        public int UserID { get; set; }


        [Required]
        [UserNotExists]
        public string Name
        {
            get;
            set;
        }

        



        [Required]
        public double Discount { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

       

    }

    public class UserNotExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TogelContext togelContext = new TogelContext();
            string[] memberNames = new string[] { validationContext.MemberName };

            var user = togelContext.Users.FirstOrDefault(u => u.Name == (string)value);

            if (user == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("Sudah ada user dengan nama tersebut", memberNames);
        }
    }
}
