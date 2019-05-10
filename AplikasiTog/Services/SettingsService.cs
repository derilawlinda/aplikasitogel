using Apel.DAL.Models;
using Apel.Services.Interfaces;
using GenericCodes.Core.Repositories;
using GenericCodes.Core.Services;
using GenericCodes.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Apel.DAL;

namespace Apel.Services
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
            List<Setting> settings = togelContext.Settings.ToList<Setting>();

            foreach(Setting setting in settings)
            {
                SettingDict.Add(setting.SettingName, setting.SettingValue);
            }
           
            return SettingDict;
        }

        public void UpdateSettings(string bettingThreshold2A, string bettingThreshold3A, string bettingThreshold4A)
        {
            string BettingThreshold2A = togelContext.Settings.Find(1).SettingValue;
            string BettingThreshold3A = togelContext.Settings.Find(2).SettingValue;
            string BettingThreshold4A = togelContext.Settings.Find(3).SettingValue;

            if (BettingThreshold2A != bettingThreshold2A)
            {
                Setting setting = togelContext.Settings.Find(1);
                setting.SettingValue = bettingThreshold2A;
            }

            if (BettingThreshold3A != bettingThreshold3A)
            {
                Setting setting = togelContext.Settings.Find(2);
                setting.SettingValue = bettingThreshold3A;
            }

            if (BettingThreshold4A != bettingThreshold4A)
            {
                Setting setting = togelContext.Settings.Find(3);
                setting.SettingValue = bettingThreshold4A;
            }

            togelContext.SaveChanges();

        }
    }
}
