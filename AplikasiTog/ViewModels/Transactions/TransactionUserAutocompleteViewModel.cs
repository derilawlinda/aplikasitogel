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
using AplikasiTog.Services;
using GenericCodes.Core.Repositories;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;

namespace AplikasiTog.ViewModels.Transactions
{
    public class TransactionUserAutocompleteViewModel : GenericCrudBase<Transaction>, INotifyPropertyChanged
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
        private readonly ITransactionsInterface _transactionService;
        public TransactionUserAutocompleteViewModel(TransactionSearchViewModel transactionSearch, ITransactionsInterface transactionService,
                                  TransactionAddEditViewModel transactionAddEditViewModel)
           : base(transactionSearch, transactionAddEditViewModel)
        {
            _transactionService = transactionService;
        }     

        TogelContext togelContext = new TogelContext();
        ViewModelLocator locator = new ViewModelLocator();

        private RelayCommand _addBetCommand;
        public RelayCommand AddBetCommand
        {
            get
            {
                return _addBetCommand
                    ?? (_addBetCommand = new RelayCommand(
                    () =>
                    {
                        int[] integers = { 1, 1, 2, 4, 5 };
                        var combinations = EnumeratePermutations2(integers);
                        var combinationList = new List<int>();
                        foreach (var combination in combinations)
                        {
                            combinationList.Add(Int32.Parse(String.Join("", combination)));
                        }
                        Transaction trx = new Transaction();
                        trx.UserID = selectedItem.UserID;
                        trx.BetAmount = 500;
                        _transactionService.InsertTransaction(trx);



                    }));
            }
        }

        static IEnumerable<int[]> EnumeratePermutations2(int[] ints)
        {
            Dictionary<int, int> intCounts = ints.GroupBy(n => n)
                                                 .ToDictionary(g => g.Key, g => g.Count());
            int[] distincts = intCounts.Keys.ToArray();
            foreach (int[] permutation in EnumeratePermutations2(new int[0], intCounts, distincts))
                yield return permutation;

        }

        static IEnumerable<int[]> EnumeratePermutations2(int[] prefix, Dictionary<int, int> intCounts, int[] distincts)
        {
            foreach (int n in distincts)
            {
                int[] newPrefix = new int[prefix.Length + 1];
                Array.Copy(prefix, newPrefix, prefix.Length);
                newPrefix[prefix.Length] = n;
                yield return newPrefix;
                intCounts[n]--;
                int[] newDistincts = intCounts[n] > 0
                                     ? distincts
                                     : distincts.Where(x => x != n).ToArray();
                foreach (int[] permutation in EnumeratePermutations2(newPrefix, intCounts, newDistincts))
                    if (permutation.Length <= 4)
                    {
                        yield return permutation;
                    }
                intCounts[n]++;

            }
        }

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
