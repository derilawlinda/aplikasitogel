using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AplikasiTog.DAL;
using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using AplikasiTog.ViewModels.Users;
using GenericCodes.CRUD.WPF.Core.MVVM;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;

namespace AplikasiTog.ViewModels.Transactions
{
    public class TransactionViewModel : GenericCrudBase<Transaction>
    {
        public TransactionViewModel(TransactionSearchViewModel transactionSearch, ITransactionsInterface transactionService,
                                  TransactionAddEditViewModel transactionAddEditViewModel)
            : base(transactionSearch, transactionAddEditViewModel)
        {
            PostDataRetrievalDelegate = (list) =>
            {
                transactionService.UpdateCanSelect(list);

                
            };          

        }

    }
    
}
