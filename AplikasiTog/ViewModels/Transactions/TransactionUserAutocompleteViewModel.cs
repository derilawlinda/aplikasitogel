using Apel.DAL;
using Apel.DAL.Models;
using GenericCodes.CRUD.WPF.Core.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Apel.Services.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Apel.Services;
using GenericCodes.Core.Repositories;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using Apel.UIServices;
using System.Data.Entity.Validation;

namespace Apel.ViewModels.Transactions
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
        
        
        private RelayCommand _addBetCommand;
        public RelayCommand AddBetCommand
        {
            get
            {
                return _addBetCommand
                    ?? (_addBetCommand = new RelayCommand(
                    () =>
                    {
                        DialogService dialog = new DialogService();
                        if (selectedItem == null)
                        {
                            dialog.ShowErrorDialog("Error", "Pilih nama akun");
                            return;
                        }

                        if (_normalBet)
                        {
                            Transaction trx = new Transaction();
                            trx.UserID = selectedItem.UserID;
                            trx.BetAmount = NormalBetAmount;
                            trx.BetNumber = BetNumber;
                            trx.Date = DateTime.Now;
                            try
                            {
                                _transactionService.InsertTransaction(trx);
                                dialog.ShowOKDialog("Info", "Taruhan terpasang");

                            }
                            catch(DbEntityValidationException ex)
                            {
                                
                                dialog.ShowErrorDialog("Error", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                            }
                        }
                        else
                        {
                            int[] integers = BetNumber.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();
                            var combinations = EnumeratePermutations2(integers);
                            var combinationList = new List<int>();
                            List<Transaction> transactionList = new List<Transaction>();
                            foreach (var combination in combinations)
                            {
                                combinationList.Add(Int32.Parse(String.Join("", combination)));
                            }
                            combinationList = combinationList.Distinct().ToList();
                            foreach (var combination in combinationList)
                            {
                                try
                                {
                                    
                                    if (!BB2AIsChecked && !BB3AIsChecked && !BB4AIsChecked)
                                    {
                                        dialog.ShowErrorDialog("Error", "Ceklis salah satu set");
                                        return;

                                    }
                                    else
                                    {
                                        if(BB2AIsChecked && BB2ABetAmout.Equals(0))
                                        {
                                            dialog.ShowErrorDialog("Error", "Nilai taruhan BB2A tidak bisa 0");
                                            return;
                                        }

                                        if (BB3AIsChecked && BB3ABetAmout.Equals(0))
                                        {
                                            dialog.ShowErrorDialog("Error", "Nilai taruhan BB3A tidak bisa 0");
                                            return;
                                        }

                                        if (BB4AIsChecked && BB4ABetAmount.Equals(0))
                                        {
                                            dialog.ShowErrorDialog("Error", "Nilai taruhan BB4A tidak bisa 0");
                                            return;
                                        }
                                    }
                                    if(combination.ToString().Length >= 2)
                                    {
                                        if (combination.ToString().Length == 2 && BB2AIsChecked)
                                        {
                                            Transaction trx = new Transaction();
                                            trx.UserID = selectedItem.UserID;
                                            trx.BetNumber = combination;
                                            trx.Date = DateTime.Now;
                                            trx.BetAmount = BB2ABetAmout;
                                            transactionList.Add(trx);

                                        }
                                        else if (combination.ToString().Length == 3 && BB3AIsChecked)
                                        {
                                            Transaction trx = new Transaction();
                                            trx.UserID = selectedItem.UserID;
                                            trx.BetNumber = combination;
                                            trx.Date = DateTime.Now;
                                            trx.BetAmount = BB3ABetAmout;
                                            transactionList.Add(trx);
                                        }
                                        else if (combination.ToString().Length == 4 && BB4AIsChecked)
                                        {
                                            Transaction trx = new Transaction();
                                            trx.UserID = selectedItem.UserID;
                                            trx.BetNumber = combination;
                                            trx.Date = DateTime.Now;
                                            trx.BetAmount = BB4ABetAmount;
                                            transactionList.Add(trx);
                                        }
                                    }
                                    
                                }catch(DbEntityValidationException ex)
                                {
                                    dialog.ShowErrorDialog("Error", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                                }
                                
                            }
                            if(transactionList.Count > 0)
                            {
                                try
                                {
                                    int BetsLength2 = transactionList.Where(t => t.BetNumber.ToString().Length == 2).Count();
                                    int BetsLength3 = transactionList.Where(t => t.BetNumber.ToString().Length == 3).Count();
                                    int BetsLength4 = transactionList.Where(t => t.BetNumber.ToString().Length == 4).Count();
                                    bool dialogResponse = dialog.ShowConfirmDialog("Info", 
                                        String.Format("Jumlah yang harus dibayarkan oleh {0} : ", SelectedItem.Name) + Environment.NewLine +
                                        String.Format("2 Angka : {0} nomor x Rp. {1} = Rp. {2} ", BetsLength2,  BB2ABetAmout.ToString("##,#"), (BetsLength2 * BB2ABetAmout).ToString("##,#")) + Environment.NewLine +
                                        String.Format("3 Angka : {0} nomor x Rp. {1} = Rp. {2} ", BetsLength3, BB3ABetAmout.ToString("##,#"), (BetsLength3 * BB3ABetAmout).ToString("##,#")) + Environment.NewLine +
                                        String.Format("4 Angka : {0} nomor x Rp. {1} = Rp. {2} ", BetsLength4, BB4ABetAmount.ToString("##,#"), (BetsLength4 * BB4ABetAmount).ToString("##,#")) + Environment.NewLine +
                                        String.Format("Total : Rp. {0} ", ((BetsLength2 * BB2ABetAmout)+ (BetsLength3 * BB3ABetAmout) + (BetsLength4 * BB4ABetAmount)).ToString("##,#")
                                        ));

                                    if (dialogResponse)
                                    {
                                        _transactionService.InsertTransactionList(transactionList);
                                        dialog.ShowOKDialog("Info", "Taruhan terpasang");
                                    }
                                    else
                                    {
                                        return;
                                    }                                    
                                }
                                catch (Exception ex)
                                {
                                    dialog.ShowOKDialog("Error", ex.Message.ToString());
                                }
                            }
                            
                                
                        }

                        


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

        double _NormalBetAmout;
        public double NormalBetAmount
        {
            get { return _NormalBetAmout; }
            set { SetField(ref _NormalBetAmout, value); }
        }

        double _BB2ABetAmout;
        public double BB2ABetAmout
        {
            get { return _BB2ABetAmout; }
            set { SetField(ref _BB2ABetAmout, value); }
        }

        double _BB3ABetAmout;
        public double BB3ABetAmout
        {
            get { return _BB3ABetAmout; }
            set { SetField(ref _BB3ABetAmout, value); }
        }

        double _BB4ABetAmount;
        public double BB4ABetAmount
        {
            get { return _BB4ABetAmount; }
            set { SetField(ref _BB4ABetAmount, value); }
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

        bool _normalBet;
        public bool NormalBet
        {
            get { return _normalBet;}
            set { SetField(ref _normalBet, value); }
        }

        int _betNumber;
        public int BetNumber
        {
            get { return _betNumber; }
            set { SetField(ref _betNumber, value); }
        }

        bool _bb2aIsChecked;
        public bool BB2AIsChecked
        {
            get { return _bb2aIsChecked; }
            set { SetField(ref _bb2aIsChecked, value); }
        }

        bool _bb3aIsChecked;
        public bool BB3AIsChecked
        {
            get { return _bb3aIsChecked; }
            set { SetField(ref _bb3aIsChecked, value); }
        }

        bool _bb4aIsChecked;
        public bool BB4AIsChecked
        {
            get { return _bb4aIsChecked; }
            set { SetField(ref _bb4aIsChecked, value); }
        }
    }
}
