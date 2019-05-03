using Apel.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.ViewModels.Transactions
{
    public class TransactionSearchViewModel : SearchCriteriaBase<Transaction>
    {
        public TransactionSearchViewModel()
           : base()
        {
        }

        private string _nameFilter = string.Empty;
        public string NameFilter
        {
            get { return _nameFilter; }
            set { Set(() => NameFilter, ref _nameFilter, value); }
        }

        public override System.Linq.Expressions.Expression<Func<Transaction, bool>> GetSearchCriteria()
        {
            return (transaction => (string.IsNullOrEmpty(_nameFilter) || transaction.User.Name.ToLower().Contains(_nameFilter.ToLower())));
        }

        public override void ResetSearchCriteria()
        {
            NameFilter = string.Empty;
        }
    }
}
