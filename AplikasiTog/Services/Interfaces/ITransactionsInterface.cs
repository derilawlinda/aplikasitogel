using Apel.DAL.Models;
using GenericCodes.Core.Services;
using System.Collections.Generic;

namespace Apel.Services.Interfaces
{
    public interface ITransactionsInterface : IService<Transaction>
    {
        void UpdateCanSelect(List<Transaction> entities);
        List<Transaction> GetALL();
        void InsertTransaction(Transaction entity);
        void InsertTransactionList(List<Transaction> entities);       
        List<TodayTransaction> GetTodayTransactions();

        List<BetRecap> GetAggregateTodayTransactions();

        List<Winning> GetWinningAggregator();

    }
}
