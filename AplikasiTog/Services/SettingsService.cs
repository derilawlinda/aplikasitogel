using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using GenericCodes.Core.Repositories;
using GenericCodes.Core.Services;
using GenericCodes.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using AplikasiTog.DAL;

namespace AplikasiTog.Services
{
    public class SettingsService : Service<Setting>, ISettingsInterface
    {
        public SettingsService(IRepository<Setting> repository) : base(repository)
        {

        }
        TogelContext togelContext = new TogelContext();
        public List<Setting> GetALL()
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var settings = unitOfWork.Repository<Setting>().List().ToList();
                return settings;
            }
        }

        public void UpdateCanSelect(List<Setting> entities)
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var settingRepo = unitOfWork.Repository<Setting>();
                List<int> usedIds = settingRepo.List().Select(s => s.SettingID).ToList();
                entities.ForEach(t =>
                {
                    t.IsSelectable = true;
                });
            }
        }

        public Dictionary<string,string> GetSettingKeyValuePairs()
        {
            Dictionary<string, string> SettingDict = new Dictionary<string, string>();
            string Winning2NomorKey = togelContext.Settings.Find(1).SettingName;
            string Winning3NomorKey = togelContext.Settings.Find(2).SettingName;
            string Winning4NomorKey = togelContext.Settings.Find(3).SettingName;
            string Winning2NomorValue = togelContext.Settings.Find(1).SettingValue;
            string Winning3NomorValue = togelContext.Settings.Find(2).SettingValue;
            string Winning4NomorValue = togelContext.Settings.Find(3).SettingValue;

            SettingDict.Add(Winning2NomorKey, Winning2NomorValue);
            SettingDict.Add(Winning3NomorKey, Winning3NomorValue);
            SettingDict.Add(Winning4NomorKey, Winning4NomorValue);

            return SettingDict;
        }

        public void UpdateSettings(string winning2Nomor, string winning3Nomor, string winning4Nomor)
        {
            string Winning2NomorValue = togelContext.Settings.Find(1).SettingValue;
            string Winning3NomorValue = togelContext.Settings.Find(2).SettingValue;
            string Winning4NomorValue = togelContext.Settings.Find(3).SettingValue;

            if(Winning2NomorValue != winning2Nomor)
            {
                Setting winning = togelContext.Settings.Find(1);
                winning.SettingValue = winning2Nomor;
            }

            if (Winning3NomorValue != winning3Nomor)
            {
                Setting winning = togelContext.Settings.Find(2);
                winning.SettingValue = winning3Nomor;
            }

            if (Winning4NomorValue != winning4Nomor)
            {
                Setting winning = togelContext.Settings.Find(3);
                winning.SettingValue = winning4Nomor;
            }

            togelContext.SaveChanges();

        }
    }
}
