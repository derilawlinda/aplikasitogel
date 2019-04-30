using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplikasiTog.Services.Interfaces;


namespace AplikasiTog.ViewModels
{
    public class MainViewModel
    {
        private readonly ITransactionsInterface _transactionService;
        public MainViewModel(ITransactionsInterface transactionService)
        {
            _transactionService = transactionService;
            
        }

        private bool isTodayTransactionExist;
        public bool IsTodayTransactionExist
        {
            get
            {
                if (_transactionService.GetTodayTransactions().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
