using AplikasiTog.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.ViewModels.Settings
{
    public class SettingSearchViewModel : SearchCriteriaBase<Setting>
    {
        public SettingSearchViewModel()
           : base()
        {
        }

        private string _settingNameFilter = string.Empty;
        public string SettingNameFilter
        {
            get { return _settingNameFilter; }
            set { Set(() => SettingNameFilter, ref _settingNameFilter, value); }
        }

        public override System.Linq.Expressions.Expression<Func<Setting, bool>> GetSearchCriteria()
        {
            return (setting => (string.IsNullOrEmpty(_settingNameFilter) || setting.SettingName.ToLower().Contains(_settingNameFilter.ToLower())));
        }

        public override void ResetSearchCriteria()
        {
            SettingNameFilter = string.Empty;
        }
    }
}
