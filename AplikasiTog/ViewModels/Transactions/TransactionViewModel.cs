using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Apel.DAL;
using Apel.DAL.Models;
using Apel.Services.Interfaces;
using Apel.ViewModels.Users;
using GenericCodes.CRUD.WPF.Core.MVVM;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using Apel.Helpers;

namespace Apel.ViewModels.Transactions
{
    public class TransactionViewModel : GenericCrudBase<Transaction>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITransactionsInterface _transactionService;
        private readonly ISettingsInterface _settingService;
        public TransactionViewModel(TransactionSearchViewModel transactionSearch, ITransactionsInterface transactionService,
                                  TransactionAddEditViewModel transactionAddEditViewModel, ISettingsInterface settingService)
            : base(transactionSearch, transactionAddEditViewModel)
        {
            _transactionService = transactionService;
            _settingService = settingService;

            PostDataRetrievalDelegate = (list) =>
            {
                transactionService.UpdateCanSelect(list);
            };
            

        }

        private List<Transaction> _transactions;

        

        public List<TodayTransaction> TodayTransactions
        {
            get
            {
                return _transactionService.GetTodayTransactions();
            }
        }


        public List<BetRecap> BetRecaps
        {
            get
            {
                var asd = _transactionService.GetAggregateTodayTransactions();
                return asd;
            }
        }

        public List<Winning> Winnings
        {
            get
            {
                return _transactionService.GetWinningAggregator();
            }
        }

        private string _todayText;

        public string TodayText
        {
            get
            {
                return String.Format("Transaksi hari {0} , {1} {2} {3}.",Helpers.Helpers.hariHelpers(Convert.ToInt16(DateTime.Now.DayOfWeek)), DateTime.Now.Day, Helpers.Helpers.bulanHelpers(Convert.ToInt16(DateTime.Now.Month)), DateTime.Now.Year);
            }
        }

        public double BettingThreshold2A
        {
            get
            {
                return Convert.ToDouble(_settingService.GetSettingKeyValuePairs()["BettingThreshold2A"]);
            }
        }

        public double BettingThreshold3A
        {
            get
            {
                return Convert.ToDouble(_settingService.GetSettingKeyValuePairs()["BettingThreshold3A"]);
            }
        }

        public double BettingThreshold4A
        {
            get
            {
                return Convert.ToDouble(_settingService.GetSettingKeyValuePairs()["BettingThreshold4A"]);
            }
        }


    }

}
