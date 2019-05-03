using Apel.DAL.Models;
using Apel.Services.Interfaces;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System.ComponentModel;

namespace Apel.ViewModels.Transactions
{
    public class TransactionAddEditViewModel : AddEditEntityBase<Transaction>
    {
        
        public TransactionAddEditViewModel()
            : base()
        {
        }
    }
}
