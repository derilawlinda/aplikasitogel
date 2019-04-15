using AplikasiTog.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;

namespace AplikasiTog.ViewModels.Users
{
    public class UserSearchViewModel : SearchCriteriaBase<User>
    {
        public UserSearchViewModel()
           : base()
        {
        }

        private string _nameFilter = string.Empty;
        public string NameFilter
        {
            get { return _nameFilter; }
            set { Set(() => NameFilter, ref _nameFilter, value); }
        }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetSearchCriteria()
        {
            return (user => (string.IsNullOrEmpty(_nameFilter) || user.Name.ToLower().Contains(_nameFilter.ToLower())));
        }

        public override void ResetSearchCriteria()
        {
            NameFilter = string.Empty;
        }
    }
}
