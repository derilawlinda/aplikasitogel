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

        



        public double Discount2A { get; set; }
        public double Discount3A { get; set; }
        public double Discount4A { get; set; }
        public double Winning2A { get; set; }
        public double Winning3A { get; set; }
        public double Winning4A { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

       

    }

    public class UserNotExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TogelContext togelContext = new TogelContext();
            if (value == null) return ValidationResult.Success;
            string[] memberNames = new string[] { validationContext.MemberName };
            var currentUser = validationContext.ObjectInstance as User;
            var user = togelContext.Users.ToList().Where(u => u.Name.ToLower() == value.ToString().ToLower() && u.UserID != currentUser.UserID).SingleOrDefault();

            if (user == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Sudah ada user dengan nama tersebut", memberNames);
            }
        }
    }
}
