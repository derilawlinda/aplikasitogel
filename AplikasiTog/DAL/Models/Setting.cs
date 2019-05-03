using GenericCodes.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.DAL.Models
{
    public class Setting : Entity
    {
        public Setting()
        {
        }

        [Key]
        public int SettingID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public int SettingType { get; set; }
    }
}
