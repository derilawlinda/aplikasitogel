using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System.ComponentModel;

namespace AplikasiTog.ViewModels.Transactions
{
    public class TransactionAddEditViewModel : AddEditEntityBase<Transaction>
    {
        
        public TransactionAddEditViewModel()
            : base()
        {
        }
    }
}
