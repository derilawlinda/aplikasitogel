using Apel.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.ViewModels.Nomors
{
    public class NomorSearchViewModel : SearchCriteriaBase<Nomor>
    {
        public NomorSearchViewModel()
           : base()
        {
        }

        private int _numberFilter = 0;
        public int NumberFilter
        {
            get { return _numberFilter; }
            set { Set(() => _numberFilter, ref _numberFilter, value); }
        }

        public override System.Linq.Expressions.Expression<Func<Nomor, bool>> GetSearchCriteria()
        {
            return (nomor => (nomor.WinningNomor.Equals(_numberFilter)));
        }

        public override void ResetSearchCriteria()
        {
            NumberFilter = 0;
        }
    }
}
