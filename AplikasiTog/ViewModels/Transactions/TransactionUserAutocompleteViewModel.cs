using AplikasiTog.DAL;
using AplikasiTog.DAL.Models;
using GenericCodes.CRUD.WPF.Core.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AplikasiTog.Services.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.ViewModels.Transactions
{
    public class TransactionUserAutocompleteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<X>.Default.Equals(field, value)) return;

            field = value;

            var h = PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public TransactionUserAutocompleteViewModel()
           : base()
        {
        }

        public ITransactionsInterface transactionService;
        
        private RelayCommand _addBetCommand;
        public RelayCommand AddBetCommand
        {
            get
            {
                return _addBetCommand
                    ?? (_addBetCommand = new RelayCommand(
                    () =>
                    {
                        TogelContext tc = new TogelContext();
                        List<Transaction> transactions = new List<Transaction>();

                        Transaction trx = new Transaction();
                        trx.User = SelectedItem;
                        trx.BetAmount = 200;
                        tc.Transactions.Add(trx);
                        tc.SaveChanges();
                    }));
            }
        }

        TogelContext togelContext = new TogelContext();
        public IReadOnlyList<User> Items
        {

            get { return togelContext.Users.ToList(); }
        }

        long? _BB2ABetAmout;
        public long? BB2ABetAmout
        {
            get { return _BB2ABetAmout; }
            set { SetField(ref _BB2ABetAmout, value); }
        }

        long? _BB3ABetAmout;
        public long? BB3ABetAmout
        {
            get { return _BB3ABetAmout; }
            set { SetField(ref _BB3ABetAmout, value); }
        }

        long? _BB4ABetAmout;
        public long? BB4ABetAmout
        {
            get { return _BB4ABetAmout; }
            set { SetField(ref _BB4ABetAmout, value); }
        }

        User selectedItem;
        public User SelectedItem
        {
            get { return selectedItem; }
            set { SetField(ref selectedItem, value); }
        }

        long? selectedValue;
        public long? SelectedValue
        {
            get { return selectedValue; }
            set { SetField(ref selectedValue, value); }
        }
    }
}
